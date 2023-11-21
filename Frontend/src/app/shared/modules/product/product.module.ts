import {NgModule} from "@angular/core";
import {CommonModule, NgOptimizedImage} from "@angular/common";
import {ProductComponent} from "./components/product/product.component";


@NgModule({
  imports: [CommonModule, NgOptimizedImage],
  declarations: [ProductComponent],
  exports: [ProductComponent],
})

export class ProductModule{}
