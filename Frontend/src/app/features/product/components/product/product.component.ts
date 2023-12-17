import {Component, OnDestroy, OnInit} from '@angular/core';
import {select, Store} from "@ngrx/store";
import {ActivatedRoute} from "@angular/router";
import {Subscription,} from "rxjs";
import {Observable} from "rxjs/internal/Observable";

import {BackendErrorsInterface} from "../../../../shared/types/backendErrors.interface";
import {errorSelector, isLoadingSelector, productSelector} from "../../store/selectors";
import {getProductAction} from "../../store/actions/getProduct.action";
import {ProductInterface} from "../../types/product.interface";



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


  images: any[] | undefined;
  rating: number = 5;

  responsiveOptions: any[] = [
    {
      breakpoint: '1024px',
      numVisible: 5
    },
    {
      breakpoint: '768px',
      numVisible: 3
    },
    {
      breakpoint: '560px',
      numVisible: 1
    }
  ];




    constructor(private store: Store, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.initializeValues()
        this.fetchData()
        this.initializeListeners()

        this.images = [
          {
          itemImageSrc: 'https://tourbaza.com/wp-content/uploads/2015/10/%D0%A7%D0%BE%D1%80%D0%BD%D0%BE%D0%B3%D0%BE%D1%80%D0%B0-1-800x401.jpg',
          thumbnailImageSrc: 'https://tourbaza.com/wp-content/uploads/2015/10/%D0%A7%D0%BE%D1%80%D0%BD%D0%BE%D0%B3%D0%BE%D1%80%D0%B0-1-800x401.jpg',
          alt: 'Description for Image 1',
          title: 'Title 1'
        },
          {
            itemImageSrc: 'https://guide.karpaty.ua/uploads/big_photos/55993f564b6172455c360000/big_a4c7ddf2-a8e6-462e-bd67-9891ec7f088d.jpg',
            thumbnailImageSrc: 'https://guide.karpaty.ua/uploads/big_photos/55993f564b6172455c360000/big_a4c7ddf2-a8e6-462e-bd67-9891ec7f088d.jpg',
            alt: 'Description for Image 1',
            title: 'Title 1'
          },
          {
            itemImageSrc: 'https://inside-ua.com/files/originals/bili-hory-2.jpg',
          },
          {
            itemImageSrc: 'https://tourbaza.com/wp-content/uploads/2015/10/%D0%A7%D0%BE%D1%80%D0%BD%D0%BE%D0%B3%D0%BE%D1%80%D0%B0-1-800x401.jpg',
            thumbnailImageSrc: 'https://tourbaza.com/wp-content/uploads/2015/10/%D0%A7%D0%BE%D1%80%D0%BD%D0%BE%D0%B3%D0%BE%D1%80%D0%B0-1-800x401.jpg',

          },
          {
            itemImageSrc: 'https://guide.karpaty.ua/uploads/big_photos/55993f564b6172455c360000/big_a4c7ddf2-a8e6-462e-bd67-9891ec7f088d.jpg',
            thumbnailImageSrc: 'https://guide.karpaty.ua/uploads/big_photos/55993f564b6172455c360000/big_a4c7ddf2-a8e6-462e-bd67-9891ec7f088d.jpg',

          },
          {
            itemImageSrc: 'https://inside-ua.com/files/originals/bili-hory-2.jpg',

          },
          {
            itemImageSrc: 'https://tourbaza.com/wp-content/uploads/2015/10/%D0%A7%D0%BE%D1%80%D0%BD%D0%BE%D0%B3%D0%BE%D1%80%D0%B0-1-800x401.jpg',
            thumbnailImageSrc: 'https://tourbaza.com/wp-content/uploads/2015/10/%D0%A7%D0%BE%D1%80%D0%BD%D0%BE%D0%B3%D0%BE%D1%80%D0%B0-1-800x401.jpg',

          }      ,    {
            itemImageSrc: 'https://tourbaza.com/wp-content/uploads/2015/10/%D0%A7%D0%BE%D1%80%D0%BD%D0%BE%D0%B3%D0%BE%D1%80%D0%B0-1-800x401.jpg',
            thumbnailImageSrc: 'https://tourbaza.com/wp-content/uploads/2015/10/%D0%A7%D0%BE%D1%80%D0%BD%D0%BE%D0%B3%D0%BE%D1%80%D0%B0-1-800x401.jpg',

          },
          {
            itemImageSrc: 'https://guide.karpaty.ua/uploads/big_photos/55993f564b6172455c360000/big_a4c7ddf2-a8e6-462e-bd67-9891ec7f088d.jpg',
            thumbnailImageSrc: 'https://guide.karpaty.ua/uploads/big_photos/55993f564b6172455c360000/big_a4c7ddf2-a8e6-462e-bd67-9891ec7f088d.jpg',
          },
          {
            itemImageSrc: 'https://inside-ua.com/files/originals/bili-hory-2.jpg',
            thumbnailImageSrc: 'https://inside-ua.com/files/originals/bili-hory-2.jpg',

          },


        ];

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
