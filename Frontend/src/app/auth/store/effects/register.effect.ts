import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {registerAction, registerFailureAction, registerSuccessAction} from "../actions/register.action";
import {catchError, of, switchMap, tap} from "rxjs";
import {AuthService} from "../../services/auth.service";
import {CurrentUserInterface} from "../../../shared/types/currentUser.interface";
import {map} from "rxjs/operators";
import {HttpErrorResponse} from "@angular/common/http";
import {PersistenceService} from "../../../shared/services/persistence.service";
import {Router} from "@angular/router";

@Injectable()

export class RegisterEffect {

  // $ - це Observable (потік даних, підписка)
  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private persistenceService: PersistenceService, //для збереження даних,
    private router: Router
  ) {}


  register$ = createEffect(() =>
    this.actions$.pipe(   //це ВСІ actions
      ofType(registerAction), //фільтруєм всі actions і отримуємо тільки registerAction
      switchMap(({request}) => {
        return this.authService.register(request).pipe(
          map((currentUser: CurrentUserInterface) => {
            this.persistenceService.set('accessToken', currentUser.token)  //збереження токіна поточного користувача
            return registerSuccessAction({currentUser})
          }),
          catchError((errorResponse: HttpErrorResponse) => {
              return of(registerFailureAction({errors: errorResponse.error.errors}))
            }
          ))
      })
    ))


  redirectAfterSubmit$ = createEffect(() =>
      this.actions$.pipe(
        ofType(registerSuccessAction),
        tap(() => {
          this.router.navigateByUrl('/')
        })
      ),
    {dispatch: false}
  )


}