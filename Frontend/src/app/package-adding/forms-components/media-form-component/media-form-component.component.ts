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
export class MediaFormComponentComponent implements OnInit {

  @ViewChild('fileUpload') fileUpload!: FileUpload;

  uploadedPictures: File[] = [];
  submitted: boolean = false;

  constructor(public ticketService: PackageAddingService, private router: Router, private messageService: MessageService) {}

  ngOnInit(): void {
    this.uploadedPictures = this.ticketService.getPackageInfo().uploadedPictures;
  }

  nextPage() {
      if (this.fileUpload && this.fileUpload.files && this.fileUpload.files.length) {
          this.uploadedPictures = this.fileUpload.files;
        this.ticketService.packageInfo.uploadedPictures = this.uploadedPictures;
        console.log(this.ticketService.getPackageInfo().uploadedPictures);
        this.router.navigate(['add-package/content']);
        return;
      }

    this.submitted = true;
  }

  prevPage() {
    this.router.navigate(['add-package/categories']);
  }
}
