import {AuthStateInterface} from "src/app/auth/types/authState.interface";
import {Action, createReducer, on} from "@ngrx/store";
import {registerAction, registerFailureAction, registerSuccessAction} from "src/app/auth/store/actions/register.action";
import {loginAction, loginFailureAction, loginSuccessAction} from "./actions/login.action";
import {state} from "@angular/animations";

const initialState: AuthStateInterface = {
  isSubmitting: false,
  currentUser: null,
  isLoggedIn: null,
  validationErrors: null
}

//Reducers - міняють стани
const authReducer = createReducer(
  initialState,  // початковий стан (не зайдено в акаунт)

  on(
    registerAction, //дія, яка міняє стан (нажали Sign in)
    (state): AuthStateInterface => ({  //міняємо стан
      ...state, //всі попередні поля не чіпаємо
      isSubmitting: true,
      validationErrors: null //треба забрати всі помилки, які були, коли ЗНОВ клікає sign in
    })
  ),

  on(
    registerSuccessAction,
    (state, action): AuthStateInterface => ({
      ...state,
      isSubmitting: false,
      isLoggedIn: true,
      currentUser: action.currentUser
    })
  ),

  on(
    registerFailureAction,
    (state, action): AuthStateInterface => ({
      ...state,
      isSubmitting: false,
      validationErrors: action.errors
    })
  ),


  on(
    loginAction,
    (state): AuthStateInterface  => ({
      ...state,
      isSubmitting: true,
      validationErrors: null
    })
  ),

  on(
    loginSuccessAction,
    (state, action): AuthStateInterface  => ({
      ...state,
      isSubmitting: false,
      currentUser: action.currentUser,
      isLoggedIn: true
    })
  ),

  on(
    loginFailureAction,
    (state, action): AuthStateInterface  => ({
      ...state,
      isSubmitting: false,
      validationErrors: action.errors
    })
  ),


)

export function reducers(state: AuthStateInterface, action: Action) {
  return authReducer(state, action)
}
