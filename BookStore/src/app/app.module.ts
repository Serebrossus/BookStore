import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JwtHelper } from 'angular2-jwt';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { GridBookComponent } from './components/grid-book/grid-book.component';
import { AddOrUpdateBookComponent } from './components/add-or-update-book/add-or-update-book.component';
import { BookService } from './services/book.service';
import { LoginComponent } from './components/login/login.component';
import { AuthGuardService } from './services/auth-guard.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    GridBookComponent,
    AddOrUpdateBookComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    BookService,
    AuthGuardService,
    JwtHelper,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
