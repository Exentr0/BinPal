import {RelatedProductsInterface} from "./relatedProducts.interface";

export interface ProductInterface {
  id: string
  name: string
  price: string
  owner: string
  rating: number
  overview: string
  publisherInfo: string
  license: string
  pictures: {
    result: string[]
  }
  relatedItems: RelatedProductsInterface[]
}
