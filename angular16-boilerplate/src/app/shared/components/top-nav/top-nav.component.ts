import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css']
})
export class TopNavComponent {
  constructor(private router: Router) { }
  logout() {
    sessionStorage.removeItem("jwt");
    sessionStorage.removeItem("user");
    this.router.navigate(['/login']);
  }
}
