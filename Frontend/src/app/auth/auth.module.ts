import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule, Routes} from "@angular/router";
import {ReactiveFormsModule} from "@angular/forms";

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';


import {RegisterComponent} from "./components/register/regoster.component";




const routes: Routes = [
  {
    path: 'register',
    component: RegisterComponent
  }
]


@NgModule({
  declarations: [RegisterComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    InputTextModule,
    PasswordModule,
    ButtonModule,
    ReactiveFormsModule
  ]
})

export class AuthModule {

}
