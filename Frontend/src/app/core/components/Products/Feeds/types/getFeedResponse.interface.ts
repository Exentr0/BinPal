import {ProductInterface} from "../../../../../shared/types/product.interface";


export interface GetFeedResponseInterface {
  // products: ProductInterface[]
  // productsCount: number


  // для тестів на https://conduit.productionready.io/api
  articles: ProductInterface[]
  articlesCount: number


}



