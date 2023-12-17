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


  // для тестів на https://conduit.productionready.io/api
  // title: string
  // // slug: string
  // body: string
  // createdAt: string
  // updatedAt: string
  // tagList: string[]
  // description: string
  // author: [{
  //   username: string
  //   bio: string | null
  //   image: string
  //   following: boolean }]
  // favorited: boolean
  // favoritesCount: number

  //для тестів на джанго
  // productId: number
  // title: string
  // description: string
  // price: number
  // quantity: number
  // pictureUrl: string


}
