import {Component, OnInit} from "@angular/core";
import {Observable} from "rxjs/internal/Observable";
import {select, Store} from "@ngrx/store";
import {isLoggedInSelector, isAnonymousSelector, currentUserSelector} from "src/app/auth/store/selectors";
import {CurrentUserInterface} from "../../types/currentUser.interface";
import {ActivatedRoute, Router} from "@angular/router";


@Component({
  selector: 'mc-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {
  isLoggedIn$!: Observable<boolean | null>
  isAnonymous$!: Observable<boolean>
  currentUser$!: Observable<CurrentUserInterface | null>
  searchText?: string

  constructor(private store: Store, private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.isLoggedIn$ = this.store.pipe(select(isLoggedInSelector))
    this.isAnonymous$ = this.store.pipe(select(isAnonymousSelector))
    this.currentUser$ = this.store.pipe(select(currentUserSelector))


  }

  onEnter() {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { q: this.searchText },
      queryParamsHandling: 'merge',
    });
  }


}
