import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {catchError, of, switchMap} from "rxjs";
import {map} from "rxjs/operators";
import {FeedService} from "../../../../services/feed.service";
import {getFeedAction, getFeedFailureAction, getFeedSuccessAction} from "../actions/getFeed.action";
import {GetFeedResponseInterface} from "../../types/getFeedResponse.interface";

@Injectable()

export class GetFeedEffect {

    constructor(
        private actions$: Actions,
        private feedService: FeedService,
    ) {
    }


    getFeedEffect = createEffect(() =>
        this.actions$.pipe(
            ofType(getFeedAction),
            switchMap(({url}) => {
                return this.feedService.getFeed(url).pipe(
                    map((feed: GetFeedResponseInterface) => {
                        return getFeedSuccessAction({feed})
                    }),
                    catchError(() => {
                            return of(getFeedFailureAction())
                        }
                    ))
            })
        ))


}

