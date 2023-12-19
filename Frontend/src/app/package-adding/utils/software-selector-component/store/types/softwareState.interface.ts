import {SoftwareInterface} from "../../../../../shared/types/software.interface";

export interface SoftwareStateInterface{
    isLoading: boolean
    error: string | null
    data: SoftwareInterface[] | null
}
