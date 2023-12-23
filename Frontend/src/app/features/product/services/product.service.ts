import {Injectable} from "@angular/core";
import {Observable} from "rxjs/internal/Observable";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {ProductInterface} from "../types/product.interface";


@Injectable()

export class ProductService {

    constructor(private http: HttpClient) {
    }

    getProduct(slug: string): Observable<ProductInterface> {
        const fullUrl = `${environment.apiUrl}/items/${slug}`
        return this.http.get<ProductInterface>(fullUrl)
    }


}
