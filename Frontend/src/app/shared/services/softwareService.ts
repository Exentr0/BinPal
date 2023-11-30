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

    // Fetch all software items from the backend
    getAllSoftware(): Observable<SoftwareInterface[]> {
        const url = environment.apiUrl + '/software/get-all-software';
        return this.http.get<any[]>(url).pipe(
            map((backendResponse) =>
                backendResponse.map((item) => ({
                    id : item.id,
                    name: item.name,
                    iconUrl: item.iconUrl,
                }))
            )
        );
    }

    // Fetch plugins for a specific software item
    getSoftwarePlugins(softwareId: number): Observable<any[]> {
        const url = `${environment.apiUrl}/software/plugins/${softwareId}`;
        return this.http.get<any[]>(url).pipe(
            map((backendResponse) =>
                backendResponse.map((item) => ({
                    id: item.id,
                    name: item.name,
                }))
            )
        );
    }

    // Fetch categories for a specific software item
    getSoftwareCategories(softwareId: number): Observable<any[]> {
        const url = `${environment.apiUrl}/software/categories/${softwareId}`;
        return this.http.get<any[]>(url).pipe(
            map((backendResponse) =>
                backendResponse.map((item) => ({
                    id: item.id,
                    name: item.name,
                }))
            )
        );
    }
}
