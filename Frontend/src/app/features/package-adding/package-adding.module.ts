import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from '@angular/router';
import { StepsModule } from 'primeng/steps';
import { ToastModule } from 'primeng/toast';
import { FormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { MessageService } from 'primeng/api';
import { PackageAddingService } from './services/packageAddingService';
import { DropdownModule } from 'primeng/dropdown';
import { MainWindowComponentComponent } from './main-window-component/main-window-component.component';
import { GeneralInfoFormComponentComponent } from './forms-components/general-info-form-component/general-info-form-component.component';
import { SupportedSoftwareFormComponent } from './forms-components/supported-software-form-component/supported-software-form-component.component';
import { RequiredPluginsFormComponent } from './forms-components/required-plugins-form-component/required-plugins-form.component';
import { SoftwareSelectorComponent } from "./utils/software-selector-component/software-selector-compoent.component";
import { SoftwarePluginsSelectorComponent } from './utils/software-plugins-selector-component/software-plugins-selector.component';
import {MultiSelectModule} from "primeng/multiselect";
import { CategoriesFormComponentComponent } from './forms-components/categories-form-component/categories-form-component.component';
import { SoftwareCategoriesSelectorComponentComponent } from './utils/software-categories-selector-component/software-categories-selector-component.component';
import { MediaFormComponentComponent } from './forms-components/media-form-component/media-form-component.component';
import {FileUploadModule} from "primeng/fileupload";
import { ContentFormComponentComponent } from './forms-components/content-form-component/content-form-component.component';
import {StoreModule} from "@ngrx/store";
import {reducers} from "./utils/software-selector-component/store/reducer";
import {EffectsModule} from "@ngrx/effects";
import {GetSoftwareEffect} from "./utils/software-selector-component/store/effects/getSoftware.effect";
import { SharedModule } from 'src/app/shared/shared.module';






// @ts-ignore
@NgModule({
  declarations: [
    MainWindowComponentComponent,
    GeneralInfoFormComponentComponent,
    SupportedSoftwareFormComponent,
    RequiredPluginsFormComponent,
    SoftwareSelectorComponent,
    SoftwarePluginsSelectorComponent,
    CategoriesFormComponentComponent,
    SoftwareCategoriesSelectorComponentComponent,
    MediaFormComponentComponent,
    ContentFormComponentComponent,
  ],
  imports: [
    CommonModule,
    StepsModule,
    ToastModule,
    CardModule,
    ButtonModule,
    InputTextModule,
    FormsModule,
    DropdownModule,
    StoreModule.forFeature('software', reducers),
    EffectsModule.forFeature([GetSoftwareEffect]),
    RouterModule.forChild([
      {
        path: 'add-package',
        component: MainWindowComponentComponent,
        children: [
          {path: 'general', component: GeneralInfoFormComponentComponent},
          {path: 'supported-software', component: SupportedSoftwareFormComponent},
          {path: 'required-plugins', component: RequiredPluginsFormComponent},
          {path: 'categories', component: CategoriesFormComponentComponent},
          {path: 'media', component: MediaFormComponentComponent},
          {path: 'content', component: ContentFormComponentComponent}
        ]
      }
    ]),
    MultiSelectModule,
    FileUploadModule,
    SharedModule,
  ],
  providers: [
    PackageAddingService,
    MessageService
  ]
})
export class PackageAddingModule { }
