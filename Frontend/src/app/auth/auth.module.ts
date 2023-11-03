import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule, Routes} from "@angular/router";
import {ReactiveFormsModule} from "@angular/forms";

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';


import {RegisterComponent} from "./components/register/regoster.component";
import {StoreModule} from "@ngrx/store";
import {reducers} from "./store/reducers";
import {AuthServise} from "./services/auth.servise";
import {EffectsModule} from "@ngrx/effects";
import {RegisterEffect} from "./store/effects/register.effect";




const routes: Routes = [
  {
    path: 'register',
    component: RegisterComponent
  }
]


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    InputTextModule,
    PasswordModule,
    ButtonModule,
    ReactiveFormsModule,
    StoreModule.forFeature('auth', reducers),
    EffectsModule.forFeature([RegisterEffect])
  ],
  declarations: [RegisterComponent],
  providers: [AuthServise]

})

export class AuthModule {

}
