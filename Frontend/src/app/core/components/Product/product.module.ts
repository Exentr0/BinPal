import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule, Routes} from "@angular/router";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { SharedModule } from "src/app/shared/shared.module";
import {DataViewModule} from "primeng/dataview";
import {SliderModule} from "primeng/slider";
import {FormsModule} from "@angular/forms";
import {InputTextModule} from "primeng/inputtext";
import {DropdownModule} from "primeng/dropdown";
import {ScrollPanelModule} from "primeng/scrollpanel";
import {GetProductEffect} from "./store/effects/getProduct.effect";
import {reducers} from "./store/reducer";
import {ProductComponent} from "./Product/product.component";
import {ProductService} from "../../services/product.service";


const routes: Routes = [
  {
    path: 'products/:slug',
    component: ProductComponent
  }
]


@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        EffectsModule.forFeature([GetProductEffect]),
        StoreModule.forFeature('product', reducers),
        RouterModule,
        SharedModule,
        DataViewModule,
        SliderModule,
        FormsModule,
        InputTextModule,
        DropdownModule,
        ScrollPanelModule,
    ],

  declarations: [ProductComponent],
    exports: [
        ProductComponent
    ],
  providers: [ProductService]
})

export class ProductModule {
}
