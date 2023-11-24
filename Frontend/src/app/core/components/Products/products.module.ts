import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import { SharedModule } from "primeng/api";
import { GlobalFeedComponent } from "./GlobalFeed/globalFeed.component";
import { EffectsModule } from "@ngrx/effects";
import { StoreModule } from "@ngrx/store";
import { FeedService } from "../../services/feed.service";
import { FeedComponent } from "./Feeds/feed.component";
import { GetFeedEffect } from "./Feeds/store/effects/getFeed.effect";
import { reducers } from "./Feeds/store/reducer";

@NgModule({
  imports: [
    CommonModule,
    EffectsModule.forFeature([GetFeedEffect]),
    StoreModule.forFeature('feed', reducers),
    RouterModule,
    SharedModule
  ],

  declarations: [FeedComponent, GlobalFeedComponent],
  exports: [FeedComponent],
  providers: [FeedService, GlobalFeedComponent]
})

export class FeedModule {
}
