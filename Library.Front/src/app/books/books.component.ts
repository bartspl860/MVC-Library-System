import { Component, OnInit, inject } from '@angular/core';
import { BooksService } from '../books.service';
import { BookResponse } from '../model/Book/book-response';
import { AuthorResponse } from '../model/Author/author-response';
import { FormBuilder, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit{
  books : BookResponse[] = [];
  authors: AuthorResponse[] = [];

  private booksService = inject(BooksService);
  private formBuilder: FormBuilder = inject(FormBuilder);

  loaded: boolean = false;
  filterForm!: FormGroup;
  
  ngOnInit(): void {
    this.booksService.get().subscribe(
      (res)=>{
        this.books = res;        
        this.loaded = true;
    });
    this.filterForm = this.formBuilder.group({
      search: ['', []]
    });
  }

  search(){
    const key: string = this.filterForm.value.search;

    this.loaded = false;
    this.books = [];

    this.booksService.getFilter(key).subscribe(
      (res)=>{
        this.books = res;
        this.loaded = true;
    });
  }
}