import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {RouterModule} from "@angular/router";
import {ErrorMessageComponent} from "./components/errorMessage/errorMessage.component";
import {BackendErrorMessagesComponent} from "./components/backendErrorMessages/backendErrorMessages.component";
import {LoadingComponent} from "./components/loading/loading.component";
import {PaginationComponent} from "./components/pagination/pagination.component";
import {PaginatorModule} from "primeng/paginator";
import {ImageModule} from "primeng/image";
import {MenubarModule} from "primeng/menubar";
import {InputTextModule} from "primeng/inputtext";
import {DividerModule} from "primeng/divider";
import {AutoFocusModule} from "primeng/autofocus";
import {ToastModule} from "primeng/toast";
import {MenuModule} from "primeng/menu";
import { KeyFilterModule } from 'primeng/keyfilter';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {ProductCardComponent} from "./components/productCard/productCard.component";


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgOptimizedImage,
    PaginatorModule,
    ImageModule,
    MenubarModule,
    InputTextModule,
    DividerModule,
    AutoFocusModule,
    ToastModule,
    MenuModule,
    KeyFilterModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    ErrorMessageComponent,
    BackendErrorMessagesComponent,
    LoadingComponent,
    PaginationComponent,
    ProductCardComponent],
  exports: [
    ErrorMessageComponent,
    BackendErrorMessagesComponent,
    LoadingComponent,
    PaginationComponent,
    ProductCardComponent],
  providers: []
})

export class SharedModule {
}
