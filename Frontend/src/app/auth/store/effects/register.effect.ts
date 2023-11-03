import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {registerAction, registerFailureAction, registerSuccessAction} from "../actions/register.action";
import {catchError, of, switchMap} from "rxjs";
import {AuthServise} from "../../services/auth.servise";
import {CurrentUserInterface} from "../../../shared/types/currentUser.interface";
import {map} from "rxjs/operators";

@Injectable()

export class RegisterEffect {

  // $ - це Observable (потік даних, підписка)
  constructor(private actions$: Actions, private authServise: AuthServise) {
  }


  register$ = createEffect(() =>
    this.actions$.pipe(   //це ВСІ actions
      ofType(registerAction), //фільтруєм всі actions і отримуємо тільки registerAction
      switchMap(({request}) => {
        return this.authServise.register(request).pipe(
          map((currentUser: CurrentUserInterface) => {
            return registerSuccessAction({currentUser})
          }),
          catchError(() => {
              return of(registerFailureAction())
            }
          ))
      })
    ))

}
