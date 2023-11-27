import {Injectable} from "@angular/core";
import {RegisterRequestInterface} from "../types/registerRequest.interface";
import {Observable} from "rxjs/internal/Observable";
import {CurrentUserInterface} from "../../shared/types/currentUser.interface";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../environments/environment";
import {AuthResponseInterface} from "../types/authResponse.interface";
import {map} from "rxjs/operators";
import {LoginRequestInterface} from "../types/loginRequest.interface";

@Injectable()



export class AuthService {
  constructor(private http: HttpClient) {}


  getUser(response: AuthResponseInterface): CurrentUserInterface {
    return response
  }

  register(data: RegisterRequestInterface): Observable<CurrentUserInterface> {
    // const url = environment.apiUrl + '/users' //для fake api
    const url = environment.apiUrl + '/Auth/register'
    return this.http.post<AuthResponseInterface>(url, data).pipe(map(this.getUser))
  }

  login(data: LoginRequestInterface): Observable<CurrentUserInterface> {
    // const url = environment.apiUrl + '/users/login'
    const url = environment.apiUrl + '/Auth/login'
    return this.http.post<AuthResponseInterface>(url, data).pipe(map(this.getUser))
  }

  getCurrentUser(): Observable<CurrentUserInterface> {
    const url = environment.apiUrl + '/user'
    return this.http.get<AuthResponseInterface>(url).pipe(map(this.getUser))
  }


}

