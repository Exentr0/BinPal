//перехоплює всі HTTP запити, отримує токен зі сховища, додає до всіх цих запитів заголовок Authorization: Token 'your_access_token'

import {Injectable} from "@angular/core";
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Observable} from "rxjs/internal/Observable";
import {PersistenceService} from "./persistence.service";

@Injectable()

export class AuthInterceptor implements HttpInterceptor {

  constructor(private persistenceService: PersistenceService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.persistenceService.get('accessToken')
    request = request.clone({
      setHeaders: {
        Authorization: token ? `Token ${token}` : ''
      }
    })
    return next.handle(request)
  }
}
