// supported-software-form-component.component.ts
import { Component, OnInit } from '@angular/core';
import { PackageAddingService } from "../../services/packageAddingService";
import { Router } from "@angular/router";
import { SoftwareInterface } from "../../../shared/types/software.interface";

@Component({
  selector: 'app-supported-software-form-component',
  templateUrl: './supported-software-form-component.component.html',
  styleUrls: ['./supported-software-form-component.component.css']
})
export class SupportedSoftwareFormComponent implements OnInit {
  supportedSoftwareList: SoftwareInterface[] = [];

  submitted: boolean = false;

  constructor(public packageAddingService: PackageAddingService, private router: Router) {}

  ngOnInit() {
    this.supportedSoftwareList = this.packageAddingService.getPackageInfo().supportedSoftwareList;
  }

  onSoftwareSelectionChange(selectedSoftwareList: SoftwareInterface[]) {
    this.supportedSoftwareList = selectedSoftwareList;
  }

  nextPage() {
    if (true) {
      this.packageAddingService.packageInfo.supportedSoftwareList = this.supportedSoftwareList;
      this.router.navigate(['add-package/required-plugins']);
      return;
    }

    this.submitted = true;
  }

  prevPage() {
    this.router.navigate(['add-package/general']);
  }
}
