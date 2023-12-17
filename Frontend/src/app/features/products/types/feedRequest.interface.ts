export interface feedRequestInterface {
  limit: number
  offset: number
  searchQuery?: string
  releaseDate?: string
  rating?: number
  software?: string[]
  categories?: string[]

  sorting?: {
    field: string
    dir: string
  }
  prices?: {
    min: number
    max: number
  }



}


