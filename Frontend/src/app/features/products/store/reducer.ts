
import {Action, createReducer, on} from "@ngrx/store";
import {getFeedAction, getFeedFailureAction, getFeedSuccessAction} from "./actions/getFeed.action";
import {routerNavigationAction} from "@ngrx/router-store";
import {FeedStateInterface} from "../types/feedState.interface";

const initialState: FeedStateInterface = {
  isLoading: false,
  error: null,
  data: null
}



const feedReducer = createReducer(
  initialState,

  on(
    getFeedAction,
    (state): FeedStateInterface => ({
      ...state,
      isLoading: true,
      error: null
    })
  ),

  on(
    getFeedSuccessAction,
    (state, action): FeedStateInterface => ({
      ...state,
      isLoading: false,
      data: action.feed
    })
  ),

  on(
    getFeedFailureAction,
    (state, action): FeedStateInterface => ({
      ...state,
      isLoading: false,
      // error:
    })
  ),

  on(
    routerNavigationAction,  //коли починається навігація
    (): FeedStateInterface => initialState)//задаємо стан на початковий



)


export function reducers(state: FeedStateInterface, action: Action) {
  return feedReducer(state, action)
}