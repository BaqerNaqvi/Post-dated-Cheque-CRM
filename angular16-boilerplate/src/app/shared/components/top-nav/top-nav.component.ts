import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css']
})
export class TopNavComponent implements OnInit {
  loggedIn: boolean = false;
  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.isAuthenticated$.subscribe((isAuthenticated) => {
      this.loggedIn = isAuthenticated;
    });
  }

  logout() {
    sessionStorage.removeItem("jwt");
    sessionStorage.removeItem("user");
    this.authService.updateAuthenticationStatus(false);
    this.router.navigate(['/login']);
  }
}
