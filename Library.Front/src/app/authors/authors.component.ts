import { Component, inject } from '@angular/core';
import { AuthorsService } from '../authors.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthorBookResponse } from '../model/Author/author-book-response';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent {
  authorBooks: AuthorBookResponse[] = [];

  private authorService = inject(AuthorsService);
  private formBuilder: FormBuilder = inject(FormBuilder);

  loaded: boolean = false;
  filterForm!: FormGroup;
  
  ngOnInit(): void {
    this.authorService.get().subscribe(
      (res)=>{      
        this.authorBooks = res;
        this.loaded = true;
    });
    this.filterForm = this.formBuilder.group({
      search: ['', []]
    });
  }

  search(){
    const key: string = this.filterForm.value.search;

    this.loaded = false;
    this.authorBooks = [];

    this.authorService.getFilter(key).subscribe(
      (res)=>{
        this.authorBooks = res;
        this.loaded = true;
    });
  }
}
