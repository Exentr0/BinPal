import {Component, Input} from "@angular/core";

@Component({
  selector: 'mc-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})

export class ProductComponent{
@Input('image') image?: string = "https://assetstorev1-prd-cdn.unity3d.com/key-image/5cb253da-59ab-40e1-b9a4-64ed12580b5e.webp"
@Input('seller') seller?: string
@Input('title') title?: string
@Input('rating') rating?: number
@Input('ratingCount') ratingCount?: number
@Input('favoritesCount') favoritesCount?: number
@Input('price') price?: number

}
