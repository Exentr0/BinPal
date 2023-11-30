import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { SoftwareInterface } from '../../shared/types/software.interface';
import { PluginInterface } from '../../shared/types/plugin.interface';
import {CategoryInterface} from "../../shared/types/category.interface";

@Injectable()
export class PackageAddingService {
    packageInfo = {
        generalInfo: {
            name: '',
            price: null,
            description: '',
        },
        supportedSoftwareList: [] as SoftwareInterface[],
        requiredPluginsMap: new Map<number, PluginInterface[]>(),
        categoriesMap: new Map<number, CategoryInterface[]>()
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
