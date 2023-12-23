import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule, Routes} from "@angular/router";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { SharedModule } from "src/app/shared/shared.module";
import {DataViewModule} from "primeng/dataview";
import {SliderModule} from "primeng/slider";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {InputTextModule} from "primeng/inputtext";
import {DropdownModule} from "primeng/dropdown";
import {ScrollPanelModule} from "primeng/scrollpanel";
import {AccordionModule} from "primeng/accordion";
import {DividerModule} from "primeng/divider";
import {CheckboxModule} from "primeng/checkbox";
import {RatingModule} from "primeng/rating";
import {RadioButtonModule} from "primeng/radiobutton";
import {ButtonModule} from "primeng/button";
import {KeyFilterModule} from "primeng/keyfilter";
import {GlobalFeedComponent} from "./components/GlobalFeed/globalFeed.component";
import {GetFeedEffect} from "./store/effects/getFeed.effect";
import {reducers} from "./store/reducer";
import {FeedComponent} from "./components/feed/feed.component";
import {TopComponent} from "./components/Top/top.component";
import {FilteringComponent} from "./components/Filtering/filtering.component";
import {FeedService} from "./services/feed.service";


const routes: Routes = [
  {
    path: 'search',
    component: GlobalFeedComponent
  }
]


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    EffectsModule.forFeature([GetFeedEffect]),
    StoreModule.forFeature('feed', reducers),
    RouterModule,
    SharedModule,
    DataViewModule,
    SliderModule,
    FormsModule,
    InputTextModule,
    DropdownModule,
    ScrollPanelModule,
    AccordionModule,
    DividerModule,
    CheckboxModule,
    RatingModule,
    RadioButtonModule,
    ButtonModule,
    KeyFilterModule,
    ReactiveFormsModule
  ],

  declarations: [FeedComponent, GlobalFeedComponent, TopComponent, FilteringComponent],
  exports: [FeedComponent],
  providers: [FeedService, GlobalFeedComponent]
})

export class ProductsModule {
}
