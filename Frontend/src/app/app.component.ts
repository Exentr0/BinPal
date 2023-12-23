import {Component, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {getCurrentUserAction} from "./features/auth/store/actions/getCurrentUser.action";
import {NavigationEnd, Router} from "@angular/router";
import {filter} from "rxjs";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{

  constructor(private store: Store, private router: Router) {}

  ngOnInit(): void {
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      // Перевіряємо, чи поточний URL після переадресації не є /login
      if (event.urlAfterRedirects !== '/login' && event.url !== '/login') {
        // Викликаємо дію getCurrentUserAction() при переході зі сторінки /login
        this.store.dispatch(getCurrentUserAction());
      }
    });
  }


}

