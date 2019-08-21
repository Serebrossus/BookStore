import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book.service';
import * as _ from 'lodash';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public booksData: Array<any>;
  public currentBook: any;

  constructor(private bookService: BookService) {
    console.log('HomeComponent');
    bookService.get().subscribe((data: any) => this.booksData = data);
    this.currentBook = this.setInitialValuesForBookData();
    console.debug({ booksData: this.booksData });
  }

  private setInitialValuesForBookData = function () {
    return {
      id: undefined,
      name: '',
      author: '',
      price: 0,
      type: ''
    }
  }

  public createOrUpdateBook = function (book: any) {
    let bookWithId;
    bookWithId = _.find(this.booksData, (el => el.id === book.id));

    if (bookWithId) {
      const updateIdx = _.findIndex(this.booksData, { id: bookWithId.id });
      this.bookService.update(book).subscribe(
        record => this.booksData.splice(updateIdx, 1, book)
      );
    } else {
      this.bookService.add(book).subscribe(
        record => this.booksData.push(book)
      );
    }

    this.currentBook = this.setInitialValuesForBookData();
  }

  public editClick = function (item) {
    this.curentBook = item;
  }

  public newClick = function () {
    this.currentBook = this.setInitialValuesForBookData();
  }

  public deleteClick = function (item) {
    const deleteIdx = _.findIndex(this.booksData, { id: item.id });
    this.bookService.splice(deleteIdx, 1);
  }

  public logOut() {
    localStorage.removeItem("jwt");
  }


  ngOnInit() {
  }

}
