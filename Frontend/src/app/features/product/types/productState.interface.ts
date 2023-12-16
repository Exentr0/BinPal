import {BackendErrorsInterface} from "../../../shared/types/backendErrors.interface";
import {ProductInterface} from "../../../shared/types/product.interface";

export interface ProductStateInterface{
    isLoading: boolean
    backendErrors: BackendErrorsInterface | null
    data: ProductInterface | null
}
