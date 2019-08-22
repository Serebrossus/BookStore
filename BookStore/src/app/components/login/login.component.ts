import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean;
  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit() {
  }

  public login(form: NgForm) {
    const credentials = JSON.stringify(form.value);
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    console.debug({ credentials, headers });
    this.http.post('http://localhost:5000/api/auth/login', credentials, {
      headers
    }).subscribe(response => {
      console.debug({response});
      const token = (<any>response).token;
      localStorage.setItem('jwt', token);
      this.invalidLogin = false;
      this.router.navigate(['/']);
    }, err => {
      console.debug({err});
      this.invalidLogin = true;
    });
  }
}
