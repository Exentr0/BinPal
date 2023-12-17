import {createAction, props} from "@ngrx/store";
import {ActionTypes} from "../actionTypes";
import {GetFeedResponseInterface} from "../../types/getFeedResponse.interface";
import {feedRequestInterface} from "../../types/feedRequest.interface";

export const getFeedAction = createAction(
    ActionTypes.GET_FEED,
    props<{url: string, feedRequest: feedRequestInterface}>()
)

export const getFeedSuccessAction = createAction(
    ActionTypes.GET_FEED_SUCCESS,
    props<{feed: GetFeedResponseInterface}>()
)

export const getFeedFailureAction = createAction(
    ActionTypes.GET_FEED_FAILURE,
)
