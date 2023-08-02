import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BookResponse } from './model/Book/book-response';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  private apiUrl = "http://localhost:64670/api/Books/";
  constructor(private http: HttpClient) { }

  get() : Observable<BookResponse[]> {
    return this.http.get<BookResponse[]>(this.apiUrl);
  }

  getFilter(key: string) : Observable<BookResponse[]>{
    return this.http.get<BookResponse[]>(this.apiUrl + "?Title=" + key);
  }
}
