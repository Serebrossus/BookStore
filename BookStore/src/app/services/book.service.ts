import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private headers: HttpHeaders;
  private accessPointUrl: string = 'http://localhost:5000/api/books';
  constructor(private http: HttpClient) {
    let token = localStorage.getItem("jwt");
    this.headers = new HttpHeaders({
      "Authorization": "Bearer " + token,
      'Content-Type': 'application/json; charset=utf-8'
    });
    console.debug({ headers: this.headers, url: this.accessPointUrl });
  }

  public get() {
    return this.http.get(this.accessPointUrl, { headers: this.headers });
  }

  public add(payload) {
    return this.http.post(this.accessPointUrl, payload, { headers: this.headers });
  }

  public remove(payload) {
    return this.http.delete(this.accessPointUrl + '/' + payload.id, { headers: this.headers });
  }

  public update(payload) {
    return this.http.put(this.accessPointUrl + '/' + payload.id, payload, { headers: this.headers });
  }
}
