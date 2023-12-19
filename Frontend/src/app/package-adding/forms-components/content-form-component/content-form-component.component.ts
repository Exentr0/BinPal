import {Component, OnInit, ViewChild} from '@angular/core';
import { FileUpload } from 'primeng/fileupload';
import { MessageService } from 'primeng/api';
import { PackageAddingService } from '../../services/packageAddingService';
import { Router } from '@angular/router';

@Component({
    selector: 'app-content-form-component',
    templateUrl: './content-form-component.component.html',
    styleUrls: ['./content-form-component.component.css'],
})
export class ContentFormComponentComponent{
    @ViewChild('fileUpload') fileUpload!: FileUpload;

    releases: File[] = [];

    constructor(public packageAddingService: PackageAddingService, private router: Router, private messageService: MessageService) {}

    onUpload(event: any){
        this.releases.push(...event.files);
    }

    complete() {
        if (this.releases.length > 0) {
            this.packageAddingService.getPackageInfo().releases = this.releases;
            this.packageAddingService.upload();
        } else {
            this.messageService.add({
                severity: 'error',
                summary: 'Error',
                detail: 'You must select at least one file.',
            });
        }
    }
}
