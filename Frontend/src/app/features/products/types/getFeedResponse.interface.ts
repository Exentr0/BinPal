import {ProductCardInterface} from "../../../shared/types/productCard.interface";


export interface GetFeedResponseInterface {
  // products: ProductInterface[]
  // productsCount: number


  // для тестів на https://conduit.productionready.io/api
  articles: ProductCardInterface[]
  articlesCount: number


}



