import {Component, OnInit} from '@angular/core';
import { MessageService } from 'primeng/api';
import { CategoryInterface } from '../../../shared/types/category.interface';
import { SoftwareInterface } from '../../../shared/types/software.interface';
import { PackageAddingService } from '../../services/packageAddingService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-categories-form-component',
  templateUrl: './categories-form-component.component.html',
  styleUrls: ['./categories-form-component.component.css'],
})
export class CategoriesFormComponentComponent implements OnInit {
  categoriesMap!: Map<number, CategoryInterface[]>;
  supportedSoftwareList!: SoftwareInterface[];

  constructor(
    public packageAddingService: PackageAddingService, private router: Router, private messageService: MessageService) {}

  ngOnInit(): void {
    this.supportedSoftwareList = this.packageAddingService.getPackageInfo().supportedSoftwareList;
    this.categoriesMap = this.packageAddingService.getPackageInfo().categoriesMap;

    this.cleanUpIfSoftwareWasRemoved();
  }

  private cleanUpIfSoftwareWasRemoved(){
    for (const softwareId of Array.from(this.categoriesMap.keys())) {
      if (!this.supportedSoftwareList.some((software) => software.id === softwareId)) {
        this.categoriesMap.delete(softwareId);
      }
    }
  }

  getPreSelectedCategories(softwareId: number): CategoryInterface[] {
    return this.categoriesMap.get(softwareId) || [];
  }

  onCategorySelectionChange(selectedCategories: CategoryInterface[], softwareId: number) {
    this.categoriesMap.set(softwareId, selectedCategories);
    console.log(this.categoriesMap)
  }

  nextPage() {
    const allCategoriesArraysNotEmpty = Array.from(this.categoriesMap.values()).every(
      (categories) => categories && categories.length >= 0
    );

    if (allCategoriesArraysNotEmpty) {
      this.packageAddingService.packageInfo.categoriesMap = this.categoriesMap;
      this.router.navigate(['add-package/media']);
    } else {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'You must choose categories for each of the supported software.',
      });
    }
  }

  prevPage() {
    this.router.navigate(['add-package/required-plugins']);
  }
}
