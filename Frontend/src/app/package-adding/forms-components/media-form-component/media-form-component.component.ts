import { Component, OnInit, ViewChild } from '@angular/core';
import { PackageAddingService } from '../../services/packageAddingService';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { FileUpload } from 'primeng/fileupload';
import {ImageService} from "../../../shared/services/imageService";

@Component({
  selector: 'app-media-form-component',
  templateUrl: './media-form-component.component.html',
  styleUrls: ['./media-form-component.component.css']
})
export class MediaFormComponentComponent implements OnInit {

  @ViewChild('fileUpload') fileUpload!: FileUpload;

  uploadedPictures: File[] = [];

  constructor(public packageAddingService: PackageAddingService, private router: Router, private messageService: MessageService, private imageService : ImageService) {}

  ngOnInit(): void {
    this.uploadedPictures = this.packageAddingService.getPackageInfo().uploadedPictures;
  }

  nextPage() {
      if (this.fileUpload && this.fileUpload.files && this.fileUpload.files.length) {
        this.uploadedPictures = this.fileUpload.files;
        this.packageAddingService.packageInfo.uploadedPictures = this.uploadedPictures;
        console.log(this.packageAddingService.getPackageInfo().uploadedPictures);
        this.router.navigate(['add-package/content']);
        return;
      }
  }


  prevPage() {
    this.router.navigate(['add-package/categories']);
  }
}
