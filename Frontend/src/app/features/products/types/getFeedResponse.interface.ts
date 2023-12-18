import {ProductCardInterface} from "../../../shared/types/productCard.interface";


export interface GetFeedResponseInterface {
  products: ProductCardInterface[]
  totalCount: number


  // для тестів на https://conduit.productionready.io/api
  // articles: ProductCardInterface[]
  // articlesCount: number


}



// export interface GetFeedResponseInterface {
//   products: ProductCardInterface[]
//   productsCount: number
//   prices?: {
//     min: number
//     max: number
//   }
//
// }
