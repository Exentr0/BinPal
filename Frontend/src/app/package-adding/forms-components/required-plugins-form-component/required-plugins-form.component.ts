import {Component, OnInit} from '@angular/core';
import { PackageAddingService } from "../../services/packageAddingService";
import { Router } from "@angular/router";
import { PluginInterface } from "../../../shared/types/plugin.interface";
import { SoftwareInterface } from "../../../shared/types/software.interface";
import {MainWindowComponentComponent} from "../../main-window-component/main-window-component.component";

@Component({
  selector: 'app-required-plugins-form-component',
  templateUrl: './required-plugins-form.component.html',
  styleUrls: ['./required-plugins-form.component.css']
})
export class RequiredPluginsFormComponent implements OnInit {
  requiredPluginsMap : Map<number, PluginInterface[]> = new Map();
  supportedSoftwareList!: SoftwareInterface[];
  constructor(public packageAddingService: PackageAddingService, private router: Router) {}

  ngOnInit(): void {
    this.supportedSoftwareList = this.packageAddingService.getPackageInfo().supportedSoftwareList;
    this.requiredPluginsMap = this.packageAddingService.getPackageInfo().requiredPluginsMap;

    this.cleanUpIfSoftwareWasRemoved();
  }

  private cleanUpIfSoftwareWasRemoved(){
    for (const softwareId of Array.from(this.requiredPluginsMap.keys())) {
      if (!this.supportedSoftwareList.some((software) => software.id === softwareId)) {
        this.requiredPluginsMap.delete(softwareId);
      }
    }
  }

    getPreSelectedPlugins(softwareId: number): PluginInterface[] {
        return this.requiredPluginsMap.get(softwareId) || [];
    }

  onPluginSelectionChange(selectedPlugins: PluginInterface[], softwareId: number) {
      this.requiredPluginsMap.set(softwareId, selectedPlugins);
  }

  nextPage() {
    if (true) {
      this.packageAddingService.packageInfo.requiredPluginsMap = this.requiredPluginsMap;
      this.router.navigate(['add-package/categories']);
      return;
    }
  }

  prevPage() {
    this.router.navigate(['add-package/supported-software']);
  }
}
