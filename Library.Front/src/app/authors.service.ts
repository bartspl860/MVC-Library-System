import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthorResponse } from './model/Author/author-response';
import { Observable } from 'rxjs';
import { BookResponse } from './model/Book/book-response';
import { AuthorBookResponse } from './model/Author/author-book-response';

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {
  private apiUrl = "http://localhost:64670/api/Authors/";
  constructor(private http: HttpClient) { }

  get() : Observable<AuthorBookResponse[]> {
    return this.http.get<AuthorBookResponse[]>(this.apiUrl);
  }

  getFilter(key: string) : Observable<AuthorBookResponse[]>{
    return this.http.get<AuthorBookResponse[]>(this.apiUrl + "?Key=" + key);
  }
}
