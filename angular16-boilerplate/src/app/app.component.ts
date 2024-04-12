import { Component } from '@angular/core';
import { version, commitHash, buildDate } from '../environments/version';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'angular16-boilerplate';
  versionInfo = { version, commitHash, buildDate };
  constructor() {
    console.log(this.versionInfo);
  }
}
