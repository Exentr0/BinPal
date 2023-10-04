import { Component } from '@angular/core';
import {UserTest} from "./modules/user-test";
import {UserTestService} from "./services/user-test.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Frontend';
  userTests : UserTest[] = [];
  constructor(private userTestService: UserTestService) { }

  ngOnInit(): void{
    this.userTestService
        .getUserTests()
        .subscribe((result: UserTest[]) => (this.userTests = result));
  }
}
