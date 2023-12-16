import {createAction, props} from "@ngrx/store";
import {ActionTypes} from "../actionTypes";
import {ProductInterface} from "../../../../shared/types/product.interface";
import {BackendErrorsInterface} from "../../../../shared/types/backendErrors.interface";



export const getProductAction = createAction(
    ActionTypes.GET_PRODUCT,
    props<{slug: string}>()
)

export const getProductSuccessAction = createAction(
    ActionTypes.GET_PRODUCT_SUCCESS,
    props<{product: ProductInterface}>()
)

export const getProductFailureAction = createAction(
    ActionTypes.GET_PRODUCT_FAILURE,
    props<{errors: BackendErrorsInterface}>()
)
