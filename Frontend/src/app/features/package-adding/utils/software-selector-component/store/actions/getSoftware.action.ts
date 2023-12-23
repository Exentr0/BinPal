import {createAction, props} from "@ngrx/store";
import {ActionTypes} from "../actionTypes";
import {SoftwareInterface} from "../../../../../../shared/types/software.interface";

export const getSoftwareAction = createAction(
    ActionTypes.GET_SOFTWARE,
)

export const getSoftwareSuccessAction = createAction(
    ActionTypes.GET_SOFTWARE_SUCCESS,
    props<{software: SoftwareInterface[]}>()
)

export const getSoftwareFailureAction = createAction(
    ActionTypes.GET_SOFTWARE_FAILURE,
    props<{error: string}>()
)
