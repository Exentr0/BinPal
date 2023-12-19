import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {registerAction, registerFailureAction, registerSuccessAction} from "../actions/register.action";
import {catchError, of, switchMap, tap} from "rxjs";
import {AuthService} from "../../services/auth.service";
import {map} from "rxjs/operators";
import {HttpErrorResponse} from "@angular/common/http";
import {Router} from "@angular/router";
import { PersistenceService } from "src/app/core/services/persistence.service";
import {CurrentUserInterface} from "../../../../shared/types/currentUser.interface";

@Injectable()

export class RegisterEffect {

  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private persistenceService: PersistenceService,
    private router: Router
  ) {}


  register$ = createEffect(() =>
    this.actions$.pipe(   //це ВСІ actions
      ofType(registerAction), //фільтруєм всі actions і отримуємо тільки registerAction
      switchMap(({request}) => {
        return this.authService.register(request).pipe(
          map((currentUser: CurrentUserInterface) => {
            this.persistenceService.set('accessToken', currentUser.accessToken)  //збереження токіна поточного користувача
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
