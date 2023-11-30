import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';

import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { ErrorMessageComponent } from './components/errorMessage/errorMessage.component';
import { BackendErrorMessagesComponent } from './components/backendErrorMessages/backendErrorMessages.component';
import { LoadingComponent } from './components/loading/loading.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { ProductComponent } from './components/product/product.component';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { UtilsService } from '../core/services/utils.service';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    MultiSelectModule,
    FormsModule,
    CardModule,
    CheckboxModule,
  ],
  declarations: [
    NavbarComponent,
    FooterComponent,
    ErrorMessageComponent,
    BackendErrorMessagesComponent,
    LoadingComponent,
    PaginationComponent,
    ProductComponent,
    SearchBarComponent,
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    ErrorMessageComponent,
    BackendErrorMessagesComponent,
    LoadingComponent,
    PaginationComponent,
    ProductComponent,
  ],
  providers: [
    UtilsService,
  ]
})
export class SharedModule { }
