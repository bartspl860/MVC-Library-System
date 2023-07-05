import { Component, OnInit } from '@angular/core';
import 'materialize-css';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  ngOnInit(): void {
    const parallaxElems = document.querySelectorAll('.parallax-container');
    M.Parallax.init(parallaxElems);
  }  
}
