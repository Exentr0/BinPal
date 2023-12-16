import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {select, Store} from "@ngrx/store";
import {getProductAction} from "../store/actions/getProduct.action";
import {ActivatedRoute} from "@angular/router";
import {Subscription,} from "rxjs";
import {ProductInterface} from "../../../../shared/types/product.interface";
import {errorSelector, isLoadingSelector, productSelector} from "../store/selectors";
import {Observable} from "rxjs/internal/Observable";
import {BackendErrorsInterface} from "../../../../shared/types/backendErrors.interface";


@Component({
    selector: 'mc-productPage',
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit, OnDestroy {

    slug!: string
    product!: ProductInterface | null
    productSubscription!: Subscription
    isLoading$!: Observable<boolean>
    backendErrors$!: Observable<BackendErrorsInterface | null>

    constructor(private store: Store, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.initializeValues()
        this.fetchData()
        this.initializeListeners()
    }

    ngOnDestroy() {
        this.productSubscription.unsubscribe()
    }

    initializeListeners(): void {
        this.productSubscription = this.store
            .pipe(select(productSelector))
            .subscribe((product: ProductInterface | null) => {
                this.product = product
            })

    }


    initializeValues(): void {
        this.slug = this.route.snapshot.paramMap.get('slug') || '';
        this.backendErrors$ = this.store.pipe(select(errorSelector))
        this.isLoading$ = this.store.pipe(select(isLoadingSelector))
    }


    fetchData(): void {
        this.store.dispatch(getProductAction({slug: this.slug}))
    }


}
