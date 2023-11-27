import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {select, Store} from "@ngrx/store";
import {Observable} from "rxjs/internal/Observable";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {of, Subscription, switchMap} from "rxjs";
import queryString from 'query-string';
import {GetFeedResponseInterface} from "./types/getFeedResponse.interface";
import {ProductInterface} from "../../../../shared/types/product.interface";
import {errorSelector, feedSelector, isLoadingSelector} from "./store/selectors";
import {getFeedAction} from "./store/actions/getFeed.action";


@Component({
  selector: 'mc-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit, OnDestroy {
  @Input('apiUrl') apiUrlProps!: string

  isLoading$!: Observable<boolean>
  error$!: Observable<string | null>
  feed$!: Observable<GetFeedResponseInterface | null>
  baseUrl!: string
  queryParamsSubscription!: Subscription
  currentPage!: number
  limitProducts!: number
  products?: ProductInterface[];

  constructor(private store: Store, private router: Router, private route: ActivatedRoute) {
  }


  ngOnInit(): void {
    this.initializeValues()
    this.initializeListeners()
  }

  ngOnDestroy(): void {
    this.queryParamsSubscription.unsubscribe()
  }


  initializeValues(): void {
    this.isLoading$ = this.store.pipe(select(isLoadingSelector))
    this.error$ = this.store.pipe(select(errorSelector))
    this.feed$ = this.store.pipe(select(feedSelector))


    this.feed$.pipe(
      switchMap(feed => feed ? of(feed) : of(null)),
    ).subscribe(feed => {
      this.products = feed?.articles;
    });


    this.baseUrl = this.router.url.split('?')[0]  //базова url до '?'
  }

  fetchFeed(): void {
    const offset = this.currentPage * this.limitProducts - this.limitProducts //зсув (індекс першого товара(в загальній множині товарів) на поточній сторінці)
    //за допомогою бібліотеки query-string створюю url з параметрами:
    const parsedUrl = queryString.parseUrl(this.apiUrlProps)
    const stringifiedParams = queryString.stringify({
      limit: this.limitProducts,
      offset,
      ...parsedUrl.query
    })
    const apiUrlWithParams = `${parsedUrl.url}?${stringifiedParams}`
    this.store.dispatch(getFeedAction({url: apiUrlWithParams}))
  }


  initializeListeners(): void {
    this.queryParamsSubscription = this.route.queryParams.subscribe(
      (params: Params) => {
        this.currentPage = Number(params['page'] || '1')
        this.limitProducts = Number(params['limit'] || '24')
        this.fetchFeed()
      })
  }


}
