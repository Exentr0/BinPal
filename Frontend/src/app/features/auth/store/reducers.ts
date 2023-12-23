import {Action, createReducer, on} from "@ngrx/store";
import {loginAction, loginFailureAction, loginSuccessAction} from "./actions/login.action";
import {
  getCurrentUserAction,
  getCurrentUserFailureAction,
  getCurrentUserSuccessAction
} from "./actions/getCurrentUser.action";
import {routerNavigationAction} from "@ngrx/router-store";
import {AuthStateInterface} from "../types/authState.interface";
import {registerAction, registerFailureAction, registerSuccessAction} from "./actions/register.action";

const initialState: AuthStateInterface = {
  isSubmitting: false,
  isLoading: false,
  currentUser: null,
  isLoggedIn: false,
  validationErrors: null
}


const authReducer = createReducer(
  initialState,

  on(
    registerAction,
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
      isLoggedIn: false,
      validationErrors: action.errors
    })
  ),


  on(
    loginAction,
    (state): AuthStateInterface => ({
      ...state,
      isSubmitting: true,
      validationErrors: null
    })
  ),

  on(
    loginSuccessAction,
    (state, action): AuthStateInterface => ({
      ...state,
      isSubmitting: false,
      isLoggedIn: true,
      currentUser: action.currentUser
    })
  ),

  on(
    loginFailureAction,
    (state, action): AuthStateInterface => ({
      ...state,
      isSubmitting: false,
      isLoggedIn: false,
      validationErrors: action.errors
    })
  ),


  on(
    getCurrentUserAction,
    (state): AuthStateInterface => ({
      ...state,
      isLoading: true,
      isLoggedIn: false,
    })
  ),

  on(
    getCurrentUserSuccessAction,
    (state, action): AuthStateInterface => ({
      ...state,
      isLoading: false,
      isLoggedIn: true,
      currentUser: action.currentUser
    })
  ),


  on(
    getCurrentUserFailureAction,
    (state): AuthStateInterface => ({
      ...state,
      isLoading: false,
      isLoggedIn: false,
      currentUser: null
    })
  ),

  on(routerNavigationAction, (): AuthStateInterface => initialState)

)


export function reducers(state: AuthStateInterface, action: Action) {
  return authReducer(state, action)
}