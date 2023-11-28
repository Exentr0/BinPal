import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import { MainWindowComponentComponent} from "./main-window-component/main-window-component.component";
import { StepsModule } from 'primeng/steps';
import { ToastModule } from 'primeng/toast';
import { GeneralInfoFormComponentComponent } from './forms-components/general-info-form-component/general-info-form-component.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import {PackageAddingService} from "./services/packageAddingService";
import { SupportedSoftwareFormComponent } from "./forms-components/supported-software-form-component/supported-software-form-component.component";
import { SharedModule} from "../shared/shared.module";
import { MessageService } from 'primeng/api';


@NgModule({
  declarations: [
    MainWindowComponentComponent,
    GeneralInfoFormComponentComponent,
    SupportedSoftwareFormComponent,
  ],
  imports: [
    CommonModule,
    StepsModule,
    ToastModule,
    CardModule,
    ButtonModule,
    InputTextModule,
    FormsModule,
    SharedModule,

    RouterModule.forChild([
      {
        path: '',
        component : MainWindowComponentComponent,
        children: [
          { path: 'general', component: GeneralInfoFormComponentComponent },
          { path: 'supported-software', component: SupportedSoftwareFormComponent }
        ]
      }
    ]),
  ],
  providers: [
      PackageAddingService,
    MessageService
  ]
})
export class PackageAddingModule { }


