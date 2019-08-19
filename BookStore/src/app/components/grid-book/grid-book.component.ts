import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-grid-book',
  templateUrl: './grid-book.component.html',
  styleUrls: ['./grid-book.component.scss']
})
export class GridBookComponent implements OnInit {
  @Input() booksData: Array<any>;
  @Output() itemDeleted = new EventEmitter<any>();
  @Output() newClick = new EventEmitter<any>();
  @Output() editClick = new EventEmitter<any>();

  constructor() { }

  public deleteItem(item) {
    this.itemDeleted.emit(item);
  }

  public editItem(item) {
    const cloneItem = Object.assign({}, item);
    this.editClick.emit(cloneItem);
  }

  public newItem() {
    this.newClick.emit();
  }

  ngOnInit() {
  }

}
