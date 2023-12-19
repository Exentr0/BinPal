import {Action, createReducer, on} from "@ngrx/store";
import {SoftwareStateInterface} from "./types/softwareState.interface";
import {getSoftwareAction, getSoftwareFailureAction, getSoftwareSuccessAction} from "./actions/getSoftware.action";


const initialState: SoftwareStateInterface = {
    isLoading: false,
    error: null,
    data: null
}

const softwareReducer = createReducer(
    initialState,

    on(
        getSoftwareAction,
        (state): SoftwareStateInterface => ({
            ...state,
            isLoading: true
        })
    ),

    on(
        getSoftwareSuccessAction,
        (state, action): SoftwareStateInterface => ({
            ...state,
            isLoading: false,
            data: action.software
        })
    ),

    on(
        getSoftwareFailureAction,
        (state, action): SoftwareStateInterface => ({
            ...state,
            isLoading: false,
            error: action.error
        })
    )
)

export function reducers(state: SoftwareStateInterface, action: Action) {
    return softwareReducer(state, action)
}

