import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { InputMaskModule } from 'primeng/inputmask';


import { RegisterComponent } from "./components/register/register.component";
import { StoreModule } from "@ngrx/store";
import { reducers } from "./store/reducers";
import { AuthService } from "./services/auth.service";
import { EffectsModule } from "@ngrx/effects";
import { RegisterEffect } from "./store/effects/register.effect";
import { LoginEffect } from "./store/effects/login.effect";
import { LoginComponent } from "./components/login/login.component";
import { GetCurrentUserEffect } from "./store/effects/getCurrentUser.effect";
import { DividerModule } from "primeng/divider";
import {SharedModule} from "../../shared/shared.module";
import {PersistenceService} from "../../core/services/persistence.service";



const routes: Routes = [
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'login',
    component: LoginComponent
  }
]


@NgModule({
  imports: [
    CommonModule,
    InputTextModule,
    PasswordModule,
    InputMaskModule,
    ButtonModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    StoreModule.forFeature('auth', reducers),
    EffectsModule.forFeature([RegisterEffect, LoginEffect, GetCurrentUserEffect]),
    SharedModule,
    FormsModule,
    DividerModule,
  ],
  declarations: [RegisterComponent, LoginComponent],
  providers: [
    AuthService,
    PersistenceService,
  ]

})

export class AuthModule {

}
