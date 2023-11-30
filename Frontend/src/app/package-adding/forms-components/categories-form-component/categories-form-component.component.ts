import {Component, OnInit} from '@angular/core';
import {CategoryInterface} from "../../../shared/types/category.interface";
import {SoftwareInterface} from "../../../shared/types/software.interface";
import {PackageAddingService} from "../../services/packageAddingService";
import {Router} from "@angular/router";
import {PluginInterface} from "../../../shared/types/plugin.interface";

@Component({
  selector: 'app-categories-form-component',
  templateUrl: './categories-form-component.component.html',
  styleUrls: ['./categories-form-component.component.css']
})
export class CategoriesFormComponentComponent implements OnInit {
  categoriesMap!: Map<number, CategoryInterface[]>
  supportedSoftwareList!: SoftwareInterface[];
  submitted: boolean = false;

  constructor(public ticketService: PackageAddingService, private router: Router) {}

  ngOnInit(): void {
    this.supportedSoftwareList = this.ticketService.getPackageInfo().supportedSoftwareList;
    this.categoriesMap = this.ticketService.getPackageInfo().categoriesMap;
  }

  getPreSelectedCategories(softwareId: number): CategoryInterface[] {
    return this.categoriesMap.get(softwareId) || [];
  }

  onCategorySelectionChange(selectedCategories: CategoryInterface[], softwareId: number) {
    this.categoriesMap.set(softwareId, selectedCategories);
  }

  nextPage() {
    if (true) {
      this.ticketService.packageInfo.categoriesMap = this.categoriesMap;
      this.router.navigate(['add-package/general']);
      return;
    }

    this.submitted = true;
  }

  prevPage() {
    this.router.navigate(['add-package/required-plugins']);
  }
}
