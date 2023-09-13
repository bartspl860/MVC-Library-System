import { AfterViewInit, Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { HeaderComponent } from '../header/header.component';
import { BooksService } from '../books.service';
import { BookResponse } from '../model/Book/book-response';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BookRequest } from '../model/Book/book-request';
import { AuthorRequest } from '../model/Author/author-request';
import { PublishingHouseRequest } from '../model/PublishingHouse/publishing-house-request';
import { AuthorResponse } from '../model/Author/author-response';
import { AuthorsService } from '../authors.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  constructor(private router: Router, private auth: AuthService,
    private booksService: BooksService, private formBuilder: FormBuilder,
    private authorService: AuthorsService) {
    const token = localStorage.getItem("session_token");
    if (token === null) {
      this.router.navigate(['login']);
    }
  }
  ngOnInit(): void {    
    this.auth.getUsername().subscribe(
      (res) => {
        this.user = res;
      },
      (error) => {
        console.log(error);
        this.auth.logout()
      }
    )
    this.authorService.get().subscribe(
      (res)=>{        
        this.authors = res;
      }
    )    
    this.addBookForm = this.formBuilder.group({
      title: ['', []],
      publishingHouse: ['', []],
      authors: []
    });     
  }
  user!: string;

  closeAll() {
    this.booksTable = false;
    this.authorsTable = false;
    this.usersTable = false;
    this.addBookForm.reset();
  }

  chips: string[] = []
  books!: BookResponse[];
  authors!: AuthorResponse[];
  toggleBooksTable() {
    this.closeAll();
    this.booksTable = true;
    this.booksService.get().subscribe(
      (res) => {
        this.books = res;
        let elems = document.querySelectorAll('select');
        M.FormSelect.init(elems);   
      }
    )
  }

  addBookForm!: FormGroup;
  addBook(){
    let authors: AuthorRequest[] = [];

    let indexes: number[] = this.addBookForm.value.authors;
    indexes.forEach((index)=>{
      let tmp = this.authors.at(index);
      if(tmp !== undefined)
        authors.push(AuthorRequest.FromAuthorResponse(tmp));
    });

    let book = new BookRequest(
      this.addBookForm.value.title,
      authors,
      new PublishingHouseRequest(this.addBookForm.value.publishingHouse)
    );

    this.booksService.add(book).subscribe(
      (res)=>{
        console.log(res);
        this.router.navigate(['/books']);
      }
    );    
  }

  toggleAuthorsTable() {
    this.closeAll();
    this.authorsTable = true;
  }

  toggleUsersTable() {
    this.closeAll();
    this.usersTable = true;
  }

  booksTable: boolean = false;//potem domyślnie false
  authorsTable: boolean = false;
  usersTable: boolean = false;
}
