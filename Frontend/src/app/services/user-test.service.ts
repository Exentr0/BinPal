import { Injectable } from '@angular/core';
import {UserTest} from '../modules/user-test'
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs/internal/Observable";
import {environment} from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UserTestService {
  private url = 'UserTest'
  constructor(private http: HttpClient) { }

  public getUserTests() : Observable<UserTest[]>{
      return this.http.get<UserTest[]>(`${environment.apiUrl}/${this.url}`);
  }
}
