import { Component } from '@angular/core';

@Component({
  selector: 'mc-global-feed',
  templateUrl: './globalFeed.component.html',
  styleUrls: ['./globalFeed.component.css']
})
export class GlobalFeedComponent {
  // apiUrl = '/products'
  apiUrl = '/item/sorting'
  // apiUrl = '/articles'
}
