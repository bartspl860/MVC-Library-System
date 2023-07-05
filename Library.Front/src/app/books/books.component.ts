import { Component, OnInit, inject } from '@angular/core';
import { BooksService } from '../books.service';
import { BookResponse } from '../model/Book/book-response';
import { AuthorResponse } from '../model/Author/author-response';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit{
  books : BookResponse[] = [];
  authors: AuthorResponse[] = [];
  private booksService = inject(BooksService);

  loaded: boolean = false;
  
  ngOnInit(): void {
    this.booksService.get().subscribe(
      (res)=>{
        this.books = res;        
        this.loaded = true;
        console.warn(this.books);
    });    
  }  
}
