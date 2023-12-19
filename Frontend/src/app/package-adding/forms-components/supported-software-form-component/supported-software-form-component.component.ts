// supported-software-form-component.component.ts
import { Component, OnInit } from '@angular/core';
import { PackageAddingService } from '../../services/packageAddingService';
import { Router } from '@angular/router';
import { SoftwareInterface } from '../../../shared/types/software.interface';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-supported-software-form-component',
  templateUrl: './supported-software-form-component.component.html',
  styleUrls: ['./supported-software-form-component.component.css'],
})
export class SupportedSoftwareFormComponent {
  supportedSoftwareList: SoftwareInterface[] = [];

  constructor(public packageAddingService: PackageAddingService, private router: Router, private messageService: MessageService) { }

  onSoftwareSelectionChange(selectedSoftwareList: SoftwareInterface[]) {
    this.supportedSoftwareList = selectedSoftwareList;
  }


  nextPage() {
    if (this.supportedSoftwareList.length > 0) {
      this.packageAddingService.packageInfo.supportedSoftwareList = this.supportedSoftwareList;
      this.router.navigate(['add-package/required-plugins']);
    } else {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'You must choose at least one supported software.',
      });
    }
  }
}
