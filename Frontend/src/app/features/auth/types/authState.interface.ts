import {CurrentUserInterface} from "../../../shared/types/currentUser.interface";
import {BackendErrorsInterface} from "../../../shared/types/backendErrors.interface";


export interface AuthStateInterface {
  isSubmitting: boolean
  isLoading: boolean
  currentUser: CurrentUserInterface | null
  isLoggedIn: boolean | null    // null - коли ще не дізналися, чи залогінений
  validationErrors: BackendErrorsInterface | null
}
