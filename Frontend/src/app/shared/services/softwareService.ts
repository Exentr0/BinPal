import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { SoftwareInterface } from '../types/software.interface';
import { environment } from "../../../environments/environment";

@Injectable({
    providedIn: 'root',
})
export class SoftwareService {
    constructor(private http: HttpClient) {}

    getAllSoftware(): Observable<SoftwareInterface[]> {
        const url = environment.apiUrl + '/Software/get-all-software';
        return this.http.get<any[]>(url).pipe(
            map((backendResponse) =>
                backendResponse.map((item) => ({
                    name: item.item1,
                    iconUrl: item.item2,
                }))
            )
        );
    }
}
