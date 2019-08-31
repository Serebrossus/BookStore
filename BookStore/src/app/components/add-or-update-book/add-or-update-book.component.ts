import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-add-or-update-book',
  templateUrl: './add-or-update-book.component.html',
  styleUrls: ['./add-or-update-book.component.scss']
})
export class AddOrUpdateBookComponent implements OnInit {
  @Output() bookCreated = new EventEmitter<any>();

  @Input() bookInfo: any;


  constructor() {
    this.clearBookInfo();
    console.log({ name: this.bookInfo.name });
  }

  private clearBookInfo = function () {
    this.bookInfo = {
      id: undefined,
      name: '',
      author: '',
      price: 0,
      type: ''
    };
  }

  public addOrUpdateBook = function (event) {
    this.bookCreated.emit(this.bookInfo);
    this.clearBookInfo();
  }

  ngOnInit() {
  }

}
