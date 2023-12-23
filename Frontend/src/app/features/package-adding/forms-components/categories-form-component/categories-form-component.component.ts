import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { PackageAddingService } from '../../services/packageAddingService';
import { Router } from '@angular/router';
import {CategoryInterface} from "../../../../shared/types/category.interface";
import {SoftwareInterface} from "../../../../shared/types/software.interface";

@Component({
  selector: 'app-categories-form-component',
  templateUrl: './categories-form-component.component.html',
  styleUrls: ['./categories-form-component.component.css'],
})
export class CategoriesFormComponentComponent implements OnInit {
  categoriesMap: Map<number, CategoryInterface[]> = new Map<number, CategoryInterface[]>();
  supportedSoftwareList: SoftwareInterface[] = [];

  constructor(
    public packageAddingService: PackageAddingService,
    private router: Router,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.supportedSoftwareList = this.packageAddingService.getPackageInfo().supportedSoftwareList;
  }

  onCategorySelectionChange(selectedCategories: CategoryInterface[], softwareId: number) {
    this.categoriesMap.set(softwareId, selectedCategories);
  }

  nextPage() {
    const allCategoriesArraysNotEmpty = Array.from(this.categoriesMap.values()).every(
      (categories) => categories && categories.length > 0
    );

    if (allCategoriesArraysNotEmpty) {
      this.packageAddingService.packageInfo.categoriesList = Array.from(this.categoriesMap.values()).flat();
      this.router.navigate(['add-package/media']);
    } else {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'You must choose categories for each of the supported software.',
      });
    }
  }
}
