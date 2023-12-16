import {Component, OnInit} from "@angular/core";
import {Observable} from "rxjs/internal/Observable";
import {select, Store} from "@ngrx/store";
import {CurrentUserInterface} from "../../types/currentUser.interface";
import {ActivatedRoute, Router} from "@angular/router";
import {currentUserSelector, isAnonymousSelector, isLoggedInSelector} from "../../../features/auth/store/selectors";


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
    this.router.navigate([''], {
      queryParams: { q: this.searchText },
      queryParamsHandling: 'merge',
    });
  }

  // onEnter() {
  //   const currentParams = { ...this.route.snapshot.queryParams };
  //   delete currentParams['page'];
  //   const mergedParams = { ...currentParams, q: this.searchText };
  //   this.router.navigate([], {
  //     relativeTo: this.route,
  //     queryParams: mergedParams,
  //     queryParamsHandling: 'merge',
  //   });
  // }



}
