import {ProductInterface} from "../../../../shared/types/product.interface";
import {BackendErrorsInterface} from "../../../../shared/types/backendErrors.interface";

export interface ProductStateInterface{
    isLoading: boolean
    backendErrors: BackendErrorsInterface | null
    data: ProductInterface | null
}
