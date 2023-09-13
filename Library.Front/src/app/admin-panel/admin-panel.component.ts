import { AfterViewInit, Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { HeaderComponent } from '../header/header.component';
import { BooksService } from '../books.service';
import { BookResponse } from '../model/Book/book-response';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookRequest } from '../model/Book/book-request';
import { AuthorRequest } from '../model/Author/author-request';
import { PublishingHouseRequest } from '../model/PublishingHouse/publishing-house-request';
import { AuthorResponse } from '../model/Author/author-response';
import { AuthorsService } from '../authors.service';
import { UsersService } from '../users.service';
import { UserResponse } from '../model/User/user-response';
import { LoginModel } from '../model/Login/login-model';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  constructor(private router: Router, private auth: AuthService,
    private booksService: BooksService, private formBuilder: FormBuilder,
    private authorService: AuthorsService, private userService: UsersService) {
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
    );
    this.authorService.get().subscribe(
      (res) => {
        this.authors = res;
      }
    );
    this.addBookForm = this.formBuilder.group({
      title: ['', []],
      publishingHouse: ['', []],
      authors: []
    });
    this.addAuthorForm = this.formBuilder.group({
      name: ['', []],
      surname: ['', []],
      dateOfBirth: ['', []]
    });
    this.addUserForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(4)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      cpassword: ['', [Validators.required]]
    });
  }
  user!: string;

  closeAll() {
    this.booksTable = false;
    this.authorsTable = false;
    this.usersTable = false;
    this.addBookForm.reset();
    this.addAuthorForm.reset();
  }

  chips: string[] = []
  books!: BookResponse[];  
  toggleBooksTable() {
    this.closeAll();
    this.booksTable = true;
    this.reloadBooks();
  }

  reloadBooks() {
    this.booksService.get().subscribe(
      (res) => {
        this.books = res;
        let elems = document.querySelectorAll('select');
        M.FormSelect.init(elems);
      }
    )
  }

  addBookForm!: FormGroup;
  addBook() {
    let authors: AuthorRequest[] = [];
    let indexes: number[] = this.addBookForm.value.authors;
    indexes.forEach((index) => {
      let tmp = this.authors.at(index);
      if (tmp !== undefined)
        authors.push(AuthorRequest.FromAuthorResponse(tmp));
    });

    let book = new BookRequest(
      this.addBookForm.value.title,
      authors,
      new PublishingHouseRequest(this.addBookForm.value.publishingHouse)
    );

    this.booksService.add(book).subscribe(
      (res) => {
        this.reloadBooks();
      }
    );
    this.addBookForm.reset();
  }

  removeBook(id: number) {
    this.booksService.remove(id).subscribe(
      (res) => {
        this.reloadBooks();
      }
    );
  }

  addAuthorForm!: FormGroup;
  addAuthor(){
    let author = new AuthorRequest(
      this.addAuthorForm.value.name,
      this.addAuthorForm.value.surname,
      this.addAuthorForm.value.dateOfBirth
    );
    this.authorService.add(author).subscribe(
      (res)=>{
        this.reloadAuthors();
      }
    );
    this.addAuthorForm.reset();
  }

  removeAuthor(id: number) {
    this.authorService.remove(id).subscribe(
      (res) => {
        this.reloadAuthors();
      }
    )
  }

  authors!: AuthorResponse[];
  toggleAuthorsTable() {
    this.closeAll();
    this.authorsTable = true;
    this.reloadAuthors();
  }

  reloadAuthors() {
    this.authorService.get().subscribe(
      (res) => {
        this.authors = res;
      }
    );
  }

  atLeastOneUser() : boolean{
    if(this.users === undefined)
      return false;
    return this.users.length > 1;
  }

  users!: UserResponse[];
  toggleUsersTable() {
    this.closeAll();
    this.usersTable = true;
    this.reloadUsers();
  }

  reloadUsers(){
    this.addUserForm.reset();
    this.userService.get().subscribe(
      (res)=>{
        this.users = res;
      }
    );
  }

  validationError: boolean = false;
  passwordNotMatch: boolean = false;
  addUserForm!: FormGroup;
  addUser(){
    let username = this.addUserForm.value.username;
    let password = this.addUserForm.value.password;
    let confirm_password = this.addUserForm.value.cpassword;

    if(!this.addUserForm.valid){
      this.validationError = true;
      return;
    }

    if(password !== confirm_password){
      this.passwordNotMatch = true;
      return;
    }

    let credentials = new LoginModel(username, password);
    this.auth.register(credentials).subscribe(
      (res)=>{
        this.reloadUsers();        
      }
    );    
  }

  removeUser(id: number){
    this.userService.delete(id).subscribe(
      (res)=>{
        this.reloadUsers();   
      }
    );
  }

  booksTable: boolean = false;//potem domyślnie false
  authorsTable: boolean = false;
  usersTable: boolean = false;
}
