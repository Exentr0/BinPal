import {Action, createReducer, on} from "@ngrx/store";
import {routerNavigationAction} from "@ngrx/router-store";
import {getProductAction, getProductFailureAction, getProductSuccessAction} from "./actions/getProduct.action";
import {ProductStateInterface} from "../types/productState.interface";

const initialState: ProductStateInterface = {
    isLoading: false,
    backendErrors: null,
    data: null
}

const productReducer = createReducer(
    initialState,

    on(
        getProductAction,
        (state): ProductStateInterface => ({
            ...state,
            isLoading: true,
        })
    ),

    on(
        getProductSuccessAction,
        (state, action): ProductStateInterface => ({
            ...state,
            isLoading: false,
            data: action.product
        })
    ),

    on(
        getProductFailureAction,
        (state, action): ProductStateInterface => ({
            ...state,
            isLoading: false,
            backendErrors: action.errors
        })
    ),

    on(routerNavigationAction, (): ProductStateInterface => initialState));


export function reducers(state: ProductStateInterface, action: Action) {
    return productReducer(state, action)
}
