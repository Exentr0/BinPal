// software-selector-compoent.component.ts
import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { SoftwareInterface } from "../../../shared/types/software.interface";
import { SoftwareService } from "../../../shared/services/softwareService";

@Component({
  selector: 'app-software-selector-component',
  templateUrl: './software-selector-compoent.component.html',
  styleUrls: ['./software-selector-compoent.component.css']
})
export class SoftwareSelectorComponent implements OnInit {
  @Output() selectionChange = new EventEmitter<SoftwareInterface[]>(); // Change any[] to SoftwareInterface[]

  softwareOptionsList: SoftwareInterface[] = [];
  @Input() selectedSoftwareList: SoftwareInterface[] = [];

  constructor(private softwareService: SoftwareService) {}

  ngOnInit(): void {
    this.loadSoftwareList();
  }

  isSelected(software: SoftwareInterface): boolean {
    return this.selectedSoftwareList?.some(s => s.id === software.id);
  }

  selectSoftware(software: SoftwareInterface): void {
    const index = this.selectedSoftwareList.findIndex(s => s.id === software.id);

    if (index !== -1) {
      // Software is already selected, so remove it from the selected list
      this.selectedSoftwareList.splice(index, 1);
    } else {
      // Software is not selected, so add it to the selected list
      this.selectedSoftwareList.push(software);
    }

    // Emit the updated selected software list
    this.selectionChange.emit(this.selectedSoftwareList);
  }

  private loadSoftwareList() {
    this.softwareService.getAllSoftware().subscribe({
      next: (data: SoftwareInterface[]) => {
        this.softwareOptionsList = data;
      },
      error: (error) => {
        console.error('Error loading software list:', error);
      }
    });
  }
}
