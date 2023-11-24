import {CurrentUserInterface} from "../../shared/types/currentUser.interface";

export interface AuthResponseInterface {
  username: string
  image: string | null
  token: string
  refreshToken: string
  // user: CurrentUserInterface
}
