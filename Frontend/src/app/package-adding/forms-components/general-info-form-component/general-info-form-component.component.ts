import {Component, OnInit} from '@angular/core';
import {PackageAddingService} from "../../services/packageAddingService";
import {Router} from "@angular/router";

@Component({
  selector: 'app-general-info-form-component',
  templateUrl: './general-info-form-component.component.html',
  styleUrls: ['./general-info-form-component.component.css']
})
export class GeneralInfoFormComponentComponent implements OnInit{
  generalInfo: any;

  submitted: boolean = false;

  constructor(public packageAddingService: PackageAddingService, private router: Router) {}

  ngOnInit() {
    this.generalInfo = this.packageAddingService.getPackageInfo().generalInfo;
  }

  nextPage() {
    if (this.generalInfo.name && this.generalInfo.price && this.generalInfo.description) {
      this.packageAddingService.packageInfo.generalInfo = this.generalInfo;
      this.router.navigate(['add-package/supported-software']);
      return;
    }

    this.submitted = true;
  }
}
