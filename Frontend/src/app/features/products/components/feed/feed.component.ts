import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {select, Store} from "@ngrx/store";
import {Observable} from "rxjs/internal/Observable";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {GetFeedResponseInterface} from "../../types/getFeedResponse.interface";
import {errorSelector, feedSelector, isLoadingSelector} from "../../store/selectors";
import {getFeedAction} from "../../store/actions/getFeed.action";
import {ProductCardInterface} from "../../../../shared/types/productCard.interface";
import {feedRequestInterface} from "../../types/feedRequest.interface";

@Component({
  selector: 'mc-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit, OnDestroy {
  @Input('apiUrl') apiUrlProps!: string

  feedRequest!: feedRequestInterface
  isLoading$!: Observable<boolean>
  error$!: Observable<string | null>
  feed$!: Observable<GetFeedResponseInterface | null>
  baseUrl!: string

  queryParamsSubscription!: Subscription
  currentPage!: number
  totalProducts!: number
  products?: ProductCardInterface[]
  rangePrice: number[] = [0, 200];


  constructor(private store: Store, private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.initializeListeners()
    this.initializeValues()
  }

  ngOnDestroy(): void {
    this.queryParamsSubscription.unsubscribe()
  }


  initializeValues(): void {
    this.isLoading$ = this.store.pipe(select(isLoadingSelector))
    this.error$ = this.store.pipe(select(errorSelector))
    this.feed$ = this.store.pipe(select(feedSelector))
    this.baseUrl = this.router.url.split('?')[0]  //базова url до '?'

    this.feed$.subscribe(response => {
      if (response) {
        this.products = response.articles;
        this.totalProducts = response.articlesCount;
      }
    });

  }

  fetchFeed(): void {
    this.store.dispatch(getFeedAction({url: this.apiUrlProps, feedRequest: this.feedRequest}))
  }


  initializeListeners(): void {
    this.queryParamsSubscription = this.route.queryParams.subscribe(
      (params: Params) => {
        this.feedRequest = {limit: 24, offset: 0}
        this.feedRequest.searchQuery = String(params['q'])
        this.feedRequest.limit = Number(params['limit'] || '24')

        this.currentPage = Number(params['page'] || '1')
        this.feedRequest.offset = this.currentPage * this.feedRequest.limit - this.feedRequest.limit
        this.fetchFeed()
      })

  }


}
