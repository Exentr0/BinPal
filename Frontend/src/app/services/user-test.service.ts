import { Injectable } from '@angular/core';
import {UserTest} from '../modules/user-test'
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs/internal/Observable";
import {EnvironmentSettings} from "../environments/environment";



@Injectable({
  providedIn: 'root'
})
export class UserTestService {
  private url = 'UserTest'
  constructor(private http: HttpClient) { }

  public getUserTests() : Observable<UserTest[]>{
      return this.http.get<UserTest[]>(`${EnvironmentSettings.apiUrl}/${this.url}`);
  }
}
