import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import {catchError, map, mergeMap} from "rxjs/operators";
import { from, of } from "rxjs";
import { getSoftwareAction, getSoftwareFailureAction, getSoftwareSuccessAction } from "../actions/getSoftware.action";
import {SoftwareService} from "../../../../../../shared/services/softwareService";

@Injectable()
export class GetSoftwareEffect {

    constructor(
        private actions$: Actions,
        private softwareService: SoftwareService,
    ) {}

    softwareOptions$ = createEffect(() =>
        this.actions$.pipe(
            ofType(getSoftwareAction),
            mergeMap(() =>
                from(this.softwareService.getAllSoftware()).pipe(
                    map((backendResponse) => getSoftwareSuccessAction({ software: backendResponse! })),
                    catchError((error) => of(getSoftwareFailureAction(error.message)))
                )
            )
        )
    );
}
