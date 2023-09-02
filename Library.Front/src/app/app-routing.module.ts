import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksComponent } from './books/books.component';
import { AuthorsComponent } from './authors/authors.component';
import { LoginComponent } from './login/login.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';

const routes: Routes = [
  { path: '', component: BooksComponent },
  { path: "books", component: BooksComponent },
  { path: "authors", component: AuthorsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin', component: AdminPanelComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
