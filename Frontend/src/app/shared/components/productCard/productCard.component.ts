import {Component, Input} from "@angular/core";

@Component({
  selector: 'mc-productCard',
  templateUrl: './productCard.component.html',
  styleUrls: ['./productCard.component.css']
})

export class ProductCardComponent {
  @Input('image') image!: string
  @Input('seller') seller?: string
  @Input('title') title?: string
  @Input('rating') rating?: number
  @Input('ratingCount') ratingCount?: number
  @Input('favoritesCount') favoritesCount?: number
  @Input('price') price?: number
}