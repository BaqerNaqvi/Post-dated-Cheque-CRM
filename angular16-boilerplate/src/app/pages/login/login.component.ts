import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string = '';
  password: string = '';
  incorrectCredentials: boolean = false;

  constructor(public authService: AuthService, private router: Router) {

  }

  ngOnInit() {
    sessionStorage.removeItem("jwt");
    sessionStorage.removeItem("user");
  }

  login() {
    if (this.username != '' && this.password != "") {
      this.authService.Login({ "username": this.username, "password": this.password }).subscribe({
        next: (result: any) => {
          sessionStorage.setItem("jwt", result.data.token);
          sessionStorage.setItem("user", result.data.user);
          this.router.navigate(['/due-payments']);
        },
        error: (e) => { console.error(e); this.incorrectCredentials = true; },
        complete: () => console.info('complete')
      });
    }
  }
}