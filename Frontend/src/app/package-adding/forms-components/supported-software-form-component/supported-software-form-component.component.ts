import {Component, OnInit} from '@angular/core';
import {PackageAddingService} from "../../services/packageAddingService";
import {Router} from "@angular/router";

@Component({
  selector: 'app-supported-software-form-component',
  templateUrl: './supported-software-form-component.component.html',
  styleUrls: ['./supported-software-form-component.component.css']
})
export class SupportedSoftwareFormComponent implements OnInit{
  supportedSoftware: any;

  submitted: boolean = false;

  constructor(public ticketService: PackageAddingService, private router: Router) {}

  ngOnInit() {
    this.supportedSoftware = this.ticketService.getPackageInfo().supportedSoftware;
  }

  nextPage() {
    if (!this.supportedSoftware.empty()){
      this.ticketService.packageInfo.supportedSoftware = this.supportedSoftware;
      this.router.navigate(['add-package/required-plugins']);
      return;
    }

    this.submitted = true;
  }
}
