import {Component, Input} from "@angular/core";
import {Router} from "@angular/router";

@Component({
  selector: 'mc-productCard',
  templateUrl: './productCard.component.html',
  styleUrls: ['./productCard.component.css']
})

export class ProductCardComponent {
  @Input('productId') productId!: string
  @Input('image') image!: string
  @Input('seller') seller?: string
  @Input('title') title?: string
  @Input('rating') rating!: number
  @Input('ratingCount') ratingCount?: number
  @Input('favoritesCount') favoritesCount?: number
  @Input('price') price?: number




}
