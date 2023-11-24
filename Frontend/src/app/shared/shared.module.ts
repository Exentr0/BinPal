import { NgModule } from "@angular/core";
import { CommonModule, NgOptimizedImage } from "@angular/common";
import { TopBarComponent } from "./components/topBar/topBar.component";
import { RouterModule } from "@angular/router";
import { ErrorMessageComponent } from "./components/errorMessage/errorMessage.component";
import { BackendErrorMessagesComponent } from "./components/backendErrorMessages/backendErrorMessages.component";
import { LoadingComponent } from "./components/loading/loading.component";
import { PaginationComponent } from "./components/pagination/pagination.component";
import { UtilsService } from "../core/services/utils.service";
import { ProductComponent } from "./components/product/product.component";

@NgModule({
  imports: [CommonModule, RouterModule, NgOptimizedImage],
  declarations: [TopBarComponent, ErrorMessageComponent, BackendErrorMessagesComponent, LoadingComponent, PaginationComponent, ProductComponent],
  exports: [TopBarComponent, ErrorMessageComponent, BackendErrorMessagesComponent, LoadingComponent, PaginationComponent, ProductComponent],
  providers: [UtilsService]
})

export class TopBarModule { }
