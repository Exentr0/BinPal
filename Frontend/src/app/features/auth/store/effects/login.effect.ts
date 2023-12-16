import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {catchError, of, switchMap, tap} from "rxjs";
import {AuthService} from "../../services/auth.service";
import {map} from "rxjs/operators";
import {HttpErrorResponse} from "@angular/common/http";
import {Router} from "@angular/router";
import {loginAction, loginFailureAction, loginSuccessAction} from "../actions/login.action";
import { PersistenceService } from "src/app/core/services/persistence.service";
import {CurrentUserInterface} from "../../../../shared/types/currentUser.interface";

@Injectable()

export class LoginEffect {

  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private persistenceService: PersistenceService,
    private router: Router
  ) {
  }


  login$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loginAction),
      switchMap(({request}) => {
        return this.authService.login(request).pipe(
          map((currentUser: CurrentUserInterface) => {
            this.persistenceService.set('accessToken', currentUser.token)
            return loginSuccessAction({currentUser})
          }),
          catchError((errorResponse: HttpErrorResponse) => {
              return of(loginFailureAction({errors: errorResponse.error.errors}))
            }
          ))
      })
    ))


    //перенаправалення на головну сторінку
  redirectAfterSubmit$ = createEffect(() =>
      this.actions$.pipe(
        ofType(loginSuccessAction),
        tap(() => {
          this.router.navigateByUrl('/')
        })
      ),
    {dispatch: false}
  )


}
