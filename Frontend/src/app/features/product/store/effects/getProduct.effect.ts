import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {catchError, of, switchMap} from "rxjs";
import {map} from "rxjs/operators";
import {getProductAction, getProductFailureAction, getProductSuccessAction} from "../actions/getProduct.action";

import {HttpErrorResponse} from "@angular/common/http";
import {ProductInterface} from "../../../../shared/types/product.interface";
import {ProductService} from "../../services/product.service";
@Injectable()

export class GetProductEffect {

    constructor(
        private actions$: Actions,
        private productService: ProductService,
    ) {
    }


    getProductEffect = createEffect(() =>
        this.actions$.pipe(
            ofType(getProductAction),
            switchMap(({slug}) => {
                return this.productService.getProduct(slug).pipe(
                    map((product: ProductInterface) => {
                        return getProductSuccessAction({product})
                    }),
                    catchError((errorResponse: HttpErrorResponse) => {
                            return of(getProductFailureAction({errors: errorResponse.error}))
                        }
                    ))
            })
        ))

}

