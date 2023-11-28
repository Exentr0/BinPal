import { NgModule } from "@angular/core";
import { CommonModule, NgOptimizedImage } from "@angular/common";
import { NavbarComponent } from "./components/navbar/navbar.component";
import { RouterModule } from "@angular/router";
import { ErrorMessageComponent } from "./components/errorMessage/errorMessage.component";
import { BackendErrorMessagesComponent } from "./components/backendErrorMessages/backendErrorMessages.component";
import { LoadingComponent } from "./components/loading/loading.component";
import { PaginationComponent } from "./components/pagination/pagination.component";
import { UtilsService } from "../core/services/utils.service";
import { ProductComponent } from "./components/product/product.component";
import { FooterComponent } from "./components/footer/footer.component";
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { MultiSelectModule } from 'primeng/multiselect';
import { SoftwareSelectorComponent } from "./components/software-selector-component/software-selector-compoent.component";
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
      CommonModule,
    RouterModule,
    NgOptimizedImage,
    MultiSelectModule,
    FormsModule
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
    SoftwareSelectorComponent],
  exports: [
    NavbarComponent,
    FooterComponent,
    ErrorMessageComponent,
    BackendErrorMessagesComponent,
    LoadingComponent,
    PaginationComponent,
    ProductComponent,
    SoftwareSelectorComponent,],
  providers: [UtilsService]
})

export class SharedModule { }
