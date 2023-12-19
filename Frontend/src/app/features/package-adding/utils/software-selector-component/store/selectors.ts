import {createFeatureSelector, createSelector} from "@ngrx/store";
import {SoftwareStateInterface} from "./types/softwareState.interface";

export const softwareFeatureSelector = createFeatureSelector<SoftwareStateInterface>('software')


export const isLoadingSelector = createSelector(
    softwareFeatureSelector,
    (softwareState: SoftwareStateInterface) => softwareState.isLoading
)


export const errorSelector = createSelector(
    softwareFeatureSelector,
    (softwareState: SoftwareStateInterface) => softwareState.error
)


export const softwareSuccessSelector = createSelector(
    softwareFeatureSelector,
    (softwareState: SoftwareStateInterface) => softwareState.data!
)
