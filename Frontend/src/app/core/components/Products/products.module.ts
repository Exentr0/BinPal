import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule, Routes} from "@angular/router";
import { GlobalFeedComponent } from "./GlobalFeed/globalFeed.component";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { FeedService } from "../../services/feed.service";
import { FeedComponent } from "./Feeds/feed.component";
import { GetFeedEffect } from "./Feeds/store/effects/getFeed.effect";
import { reducers } from "./Feeds/store/reducer";
import { SharedModule } from "src/app/shared/shared.module";
import {DataViewModule} from "primeng/dataview";
import {SliderModule} from "primeng/slider";
import {FormsModule} from "@angular/forms";
import {InputTextModule} from "primeng/inputtext";
import {DropdownModule} from "primeng/dropdown";
import {TopComponent} from "./Top/top.component";
import {ScrollPanelModule} from "primeng/scrollpanel";
import {FilteringComponent} from "./Filtering/filtering.component";
import {AccordionModule} from "primeng/accordion";
import {DividerModule} from "primeng/divider";
import {CheckboxModule} from "primeng/checkbox";
import {RatingModule} from "primeng/rating";
import {RadioButtonModule} from "primeng/radiobutton";


const routes: Routes = [
  {
    path: '',
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
    RadioButtonModule
  ],

  declarations: [FeedComponent, GlobalFeedComponent, TopComponent, FilteringComponent],
  exports: [FeedComponent],
  providers: [FeedService, GlobalFeedComponent]
})

export class ProductsModule {
}
