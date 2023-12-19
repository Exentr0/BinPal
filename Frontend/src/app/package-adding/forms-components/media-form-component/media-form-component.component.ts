import { Component, OnInit, ViewChild } from '@angular/core';
import { PackageAddingService } from '../../services/packageAddingService';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { FileUpload } from 'primeng/fileupload';

@Component({
  selector: 'app-media-form-component',
  templateUrl: './media-form-component.component.html',
  styleUrls: ['./media-form-component.component.css']
})
export class MediaFormComponentComponent {

  @ViewChild('fileUpload') fileUpload!: FileUpload;

  uploadedPictures: File[] = [];

  constructor(public packageAddingService: PackageAddingService, private router: Router, private messageService: MessageService) {}


  onUpload(event: any){
    this.uploadedPictures.push(...event.files)
  }

  nextPage() {
    if (this.uploadedPictures.length > 0) {
      this.packageAddingService.packageInfo.uploadedPictures = this.uploadedPictures;
      this.router.navigate(['add-package/content']);
      return;
    }
    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: 'You must upload at least one picture or video.',
    });
  }
}
