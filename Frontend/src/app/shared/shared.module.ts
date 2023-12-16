import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {NavbarComponent} from "./components/navbar/navbar.component";
import {RouterModule} from "@angular/router";
import {ErrorMessageComponent} from "./components/errorMessage/errorMessage.component";
import {BackendErrorMessagesComponent} from "./components/backendErrorMessages/backendErrorMessages.component";
import {LoadingComponent} from "./components/loading/loading.component";
import {PaginationComponent} from "./components/pagination/pagination.component";
import {UtilsService} from "../core/services/utils.service";
import {ProductComponent} from "./components/product/product.component";
import {FooterComponent} from "./components/footer/footer.component";
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
    NavbarComponent,
    FooterComponent,
    ErrorMessageComponent,
    BackendErrorMessagesComponent,
    LoadingComponent,
    PaginationComponent,
    ProductComponent],
  exports: [
    NavbarComponent,
    FooterComponent,
    ErrorMessageComponent,
    BackendErrorMessagesComponent,
    LoadingComponent,
    PaginationComponent,
    ProductComponent],
  providers: [UtilsService]
})

export class SharedModule {
}
