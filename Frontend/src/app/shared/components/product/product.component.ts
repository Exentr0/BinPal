import {Component, Input} from "@angular/core";

@Component({
  selector: 'mc-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})

export class ProductComponent {
  @Input('image') image!: string
  @Input('seller') seller?: string
  @Input('title') title?: string
  @Input('rating') rating?: number
  @Input('ratingCount') ratingCount?: number
  @Input('favoritesCount') favoritesCount?: number
  @Input('price') price?: number
}
