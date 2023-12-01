import {Component, OnInit, ViewChild} from '@angular/core';
import { FileUpload } from 'primeng/fileupload';
import { MessageService } from 'primeng/api';
import { PackageAddingService } from '../../services/packageAddingService';
import { Router } from '@angular/router';
import * as JSZip from 'jszip';

@Component({
    selector: 'app-content-form-component',
    templateUrl: './content-form-component.component.html',
    styleUrls: ['./content-form-component.component.css'],
})
export class ContentFormComponentComponent implements OnInit{
    @ViewChild('fileUpload') fileUpload!: FileUpload;

    releases: File[] = [];
    submitted: boolean = false;

    constructor(public packageAddingService: PackageAddingService, private router: Router, private messageService: MessageService) {}

    ngOnInit(): void {
      this.releases = this.packageAddingService.getPackageInfo().releases
    }

    nextPage() {
        if (true) {
            this.releases = this.fileUpload.files;
            this.packageAddingService.getPackageInfo().releases = this.releases;
            this.router.navigate(['add-package/general']);
            return;
        }

        this.submitted = true;
    }

    complete() {
        if (true) {

            this.packageAddingService.getPackageInfo().releases = this.releases;

            this.packageAddingService.upload();
        } else {
            console.error('Files exceed the maximum allowed size.');
        }
    }

    prevPage() {
        this.router.navigate(['add-package/media']);
    }
}
