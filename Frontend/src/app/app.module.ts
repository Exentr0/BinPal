import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {AppRoutingModule} from "./app-routing.module";
import {AppComponent} from './app.component';
import {AuthModule} from "./auth/auth.module";
import {StoreModule} from "@ngrx/store";
import {StoreDevtoolsModule} from "@ngrx/store-devtools";
import {environment} from "src/environments/environment";
import {HttpClientModule} from "@angular/common/http";
import { EffectsModule } from '@ngrx/effects';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AuthModule,
    HttpClientModule,
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({  //для розширення браузера, щоб моніторити NgRx
      maxAge: 25, //максимальна кількість дозволених дій для збереження в дереві історії
      logOnly: environment.production //працює тільки в режимі розробки
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
