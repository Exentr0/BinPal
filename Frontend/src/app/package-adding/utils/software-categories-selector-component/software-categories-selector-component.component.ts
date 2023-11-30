import {Component, EventEmitter, Input, Output} from '@angular/core';
import {SoftwareInterface} from "../../../shared/types/software.interface";
import {CategoryInterface} from "../../../shared/types/category.interface";
import {SoftwareService} from "../../../shared/services/softwareService";

@Component({
  selector: 'app-software-categories-selector-component',
  templateUrl: './software-categories-selector-component.component.html',
  styleUrls: ['./software-categories-selector-component.component.css']
})
export class SoftwareCategoriesSelectorComponentComponent {
  @Input() software!: SoftwareInterface;
  categoryOptions: CategoryInterface[] = [];
  @Input() preSelectedCategories: CategoryInterface[] = [];
  @Output() selectionChange = new EventEmitter<CategoryInterface[]>();

  selectedCategories : CategoryInterface[] = [];

  constructor(private softwareService: SoftwareService) {}

  ngOnInit(): void {
    this.fetchSoftwareCategories();
    this.selectedCategories = [...this.preSelectedCategories];
    console.log('Selected plugins:', this.selectedCategories);
  }

  onSelectionChange() {
    this.selectionChange.emit(this.selectedCategories);
  }

  private fetchSoftwareCategories() {
    this.softwareService.getSoftwareCategories(this.software.id).subscribe(
        (categories) => {
          this.categoryOptions = categories;
          console.log(this.categoryOptions)
        },
        (error) => {
          console.error('Error fetching software categories:', error);
        }
    );
  }
}
