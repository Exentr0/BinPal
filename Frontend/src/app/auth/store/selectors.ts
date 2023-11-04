//отримання частини стану (isSubmitting зі стану 'auth')

import {createFeatureSelector, createSelector} from "@ngrx/store";
import {AuthStateInterface} from "src/app/auth/types/authState.interface";


export const authFeatureSelector = createFeatureSelector<AuthStateInterface>('auth')
//Створений селектор authFeatureSelector дозволяє отримати доступ до стану фічі 'auth'


export const isSubmittingSelector = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.isSubmitting
)

export const validationErrorsSelector = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.validationErrors
)



//   для топбар:
export const isLoggedInSelector = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.isLoggedIn
)

export const isAnonymousSelector = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.isLoggedIn === false
)

export const currentUserSelector = createSelector(
  authFeatureSelector,
  (authState: AuthStateInterface) => authState.currentUser
)


// createSelector - створює більш складний селектор за допомогою комбінації інших селекторів чи простих функцій
// для обчислення значень на основі стану



