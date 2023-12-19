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
  @Output() selectionChange = new EventEmitter<CategoryInterface[]>();

  selectedCategories : number[] = [];

  constructor(private softwareService: SoftwareService) {}

  ngOnInit(): void {
    this.fetchSoftwareCategories();
  }

  onSelectionChange() {
    let selected = this.categoryOptions.filter(c => this.selectedCategories.some(sc => sc == c.id));
    this.selectionChange.emit(selected);
  }

  private fetchSoftwareCategories() {
    this.softwareService.getSoftwareCategories(this.software.id).subscribe(
        (categories) => {
          this.categoryOptions = categories;
        },
        (error) => {
          console.error('Error fetching software categories:', error);
        }
    );
  }
}
