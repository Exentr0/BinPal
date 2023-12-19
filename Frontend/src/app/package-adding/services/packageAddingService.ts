import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { SoftwareInterface } from '../../shared/types/software.interface';
import { PluginInterface } from '../../shared/types/plugin.interface';
import { CategoryInterface } from '../../shared/types/category.interface';
import { HttpClient } from '@angular/common/http';
import { environment } from "../../../environments/environment";

@Injectable()
export class PackageAddingService {
    packageInfo = {
        generalInfo: {
            name: '',
            price: null,
            description: '',
        },
        supportedSoftwareList: [] as SoftwareInterface[],
        requiredPluginsList: [] as PluginInterface[],
        categoriesList: [] as CategoryInterface[],
        uploadedPictures: [] as File[],
        releases:[] as File[],
    };

    constructor(private http: HttpClient) {}

    private packageUploaded = new Subject<any>();
    packageUploaded$ = this.packageUploaded.asObservable();

    getPackageInfo() {
        return this.packageInfo;
    }

    upload() {
        const url = environment.apiUrl + "/Item/upload-package";

        const formData: FormData = new FormData();



        // Append files to FormData
        this.packageInfo.uploadedPictures.forEach((file, index) => {
            formData.append(`UploadedPictures[${index}]`, file);
        });

        this.packageInfo.releases.forEach((file, index) => {
            formData.append(`Releases[${index}]`, file);
        });

        this.packageInfo.releases = [];
        this.packageInfo.uploadedPictures = [];
        // Append data to FormData
        formData.append('packageInfo', JSON.stringify(this.packageInfo));

        console.log(formData)

        this.http.post(url, formData).subscribe(
            (response) => {
                console.log('Package uploaded successfully', response);
                this.packageUploaded.next({ packageInfo: this.packageInfo });
            },
            (error) => {
                console.error('Error uploading package', error);
            }
        );
    }
}
