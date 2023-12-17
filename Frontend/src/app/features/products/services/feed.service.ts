import {Injectable} from "@angular/core";
import {Observable} from "rxjs/internal/Observable";
import {HttpClient} from "@angular/common/http";
import { environment } from "src/environments/environment";
import {GetFeedResponseInterface} from "../types/getFeedResponse.interface";
import {feedRequestInterface} from "../types/feedRequest.interface";
import {map} from "rxjs/operators";

@Injectable()

export class FeedService{

    constructor(private http: HttpClient) {}
    getFeed(url: string, feedRequest: feedRequestInterface): Observable<GetFeedResponseInterface>{
        const fullUrl = environment.apiUrl + url
        return this.http.post<GetFeedResponseInterface>(fullUrl, feedRequest)
          .pipe(
            map((response: GetFeedResponseInterface) =>{
              return response
            })
            )
    }
}
