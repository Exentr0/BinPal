import {Component, OnInit} from "@angular/core";
import {Observable} from "rxjs/internal/Observable";
import {CurrentUserInterface} from "../../types/currentUser.interface";
import {select, Store} from "@ngrx/store";
import {currentUserSelector, isAnonymousSelector, isLoggedInSelector} from "../../../features/auth/store/selectors";

@Component({
  selector: 'mc-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})

export class FooterComponent implements OnInit {
  isLoggedIn$!: Observable<boolean | null>
  isAnonymous$!: Observable<boolean>
  currentUser$!: Observable<CurrentUserInterface | null>

  constructor(private store: Store) {
  }

  ngOnInit(): void {
    this.isLoggedIn$ = this.store.pipe(select(isLoggedInSelector))
    this.isAnonymous$ = this.store.pipe(select(isAnonymousSelector))
    this.currentUser$ = this.store.pipe(select(currentUserSelector))
  }
}
