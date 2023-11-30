import {Component, OnInit} from "@angular/core";
import {Observable} from "rxjs/internal/Observable";
import {select, Store} from "@ngrx/store";
import {isLoggedInSelector, isAnonymousSelector, currentUserSelector} from "src/app/auth/store/selectors";
import {CurrentUserInterface} from "../../types/currentUser.interface";
import {MenuItem} from "primeng/api";


@Component({
  selector: 'mc-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {
  isLoggedIn$!: Observable<boolean | null>
  isAnonymous$!: Observable<boolean>
  currentUser$!: Observable<CurrentUserInterface | null>
  value?: any
  items: MenuItem[] | undefined;

  constructor(private store: Store) {
  }

  ngOnInit(): void {
    this.isLoggedIn$ = this.store.pipe(select(isLoggedInSelector))
    this.isAnonymous$ = this.store.pipe(select(isAnonymousSelector))
    this.currentUser$ = this.store.pipe(select(currentUserSelector))

    this.items = [
      {
        label: 'Options',
        items: [
          {
            label: 'Update',
            icon: 'pi pi-refresh',

          },
          {
            label: 'Delete',
            icon: 'pi pi-times',

          }
        ]
      },
      {
        label: 'Navigate',
        items: [
          {
            label: 'Angular',
            icon: 'pi pi-external-link',
            url: 'http://angular.io'
          },
          {
            label: 'Router',
            icon: 'pi pi-upload',
            routerLink: '/fileupload'
          }
        ]
      }
    ];
  }


}
