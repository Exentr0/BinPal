import { Injectable } from '@angular/core';
import {Subject} from "rxjs";
import {SoftwareInterface} from "../../shared/types/software.interface";


@Injectable()
export class PackageAddingService {
    packageInfo = {
        generalInfo : {
            name : '',
            price: null,
            description : ''
        },
        supportedSoftware: {
            supportedSoftwareList: [] as SoftwareInterface[]
        }
    };

    private packageUploaded = new Subject<any>();

    packageUploaded$ = this.packageUploaded.asObservable();


    getPackageInfo() {
        return this.packageInfo;
    }

    uploaded() {
        this.packageUploaded.next(this.packageInfo.generalInfo);
    }
}
