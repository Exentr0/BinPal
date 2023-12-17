import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule, Routes} from "@angular/router";
import {EffectsModule} from "@ngrx/effects";
import {StoreModule} from "@ngrx/store";
import {SharedModule} from "src/app/shared/shared.module";
import {DataViewModule} from "primeng/dataview";
import {SliderModule} from "primeng/slider";
import {FormsModule} from "@angular/forms";
import {InputTextModule} from "primeng/inputtext";
import {DropdownModule} from "primeng/dropdown";
import {ScrollPanelModule} from "primeng/scrollpanel";
import {GetProductEffect} from "./store/effects/getProduct.effect";
import {reducers} from "./store/reducer";
import {ProductComponent} from "./components/product/product.component";
import {ProductService} from "./services/product.service";
import {GalleriaModule} from "primeng/galleria";
import {ButtonModule} from "primeng/button";
import {RatingModule} from "primeng/rating";
import {BreadcrumbModule} from "primeng/breadcrumb";
import {TabViewModule} from "primeng/tabview";
import {DividerModule} from "primeng/divider";


const routes: Routes = [
  {
    path: 'items/:slug',
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
    ButtonModule,
    SliderModule,
    FormsModule,
    InputTextModule,
    DropdownModule,
    ScrollPanelModule,
    GalleriaModule,
    RatingModule,
    BreadcrumbModule,
    TabViewModule,
    DividerModule,
  ],

  declarations: [ProductComponent],
  exports: [
    ProductComponent
  ],
  providers: [ProductService]
})

export class ProductModule {
}
