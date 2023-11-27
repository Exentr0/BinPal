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

@NgModule({
  imports: [CommonModule, RouterModule, NgOptimizedImage],
  declarations: [
    NavbarComponent,
    FooterComponent,
    ErrorMessageComponent,
    BackendErrorMessagesComponent,
    LoadingComponent,
    PaginationComponent,
    ProductComponent,
    SearchBarComponent],
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

export class SharedModule { }
