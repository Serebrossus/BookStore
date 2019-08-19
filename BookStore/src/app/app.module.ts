import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { GridBookComponent } from './components/grid-book/grid-book.component';
import { AddOrUpdateBookComponent } from './components/add-or-update-book/add-or-update-book.component';
import { BookService } from './services/book.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GridBookComponent,
    AddOrUpdateBookComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    BookService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
