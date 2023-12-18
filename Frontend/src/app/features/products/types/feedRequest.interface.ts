export interface feedRequestInterface {
  limit: number
  offset: number
  searchQuery: string | null
  releaseDate: string | null
  sorting: number
  minRating: number | null
  maxPrice: number | null
  minPrice: number | null


  software?: string[]
  categories?: string[]

  // sorting?: {
  //   field: string
  //   dir: string
  // }
  // prices?: {
  //   min: number
  //   max: number
  // }



}


