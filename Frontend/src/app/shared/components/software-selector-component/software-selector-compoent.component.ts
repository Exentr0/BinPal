import { Component, OnInit } from '@angular/core';
import { SoftwareInterface } from "../../types/software.interface";
import { SoftwareService } from "../../services/softwareService";

@Component({
  selector: 'app-software-selector-component',
  templateUrl: './software-selector-compoent.component.html',
  styleUrls: ['./software-selector-compoent.component.css']
})
export class SoftwareSelectorComponent implements OnInit {
  softwareList!: SoftwareInterface[];
  selectedSoftware!: SoftwareInterface[];


  constructor(private softwareService: SoftwareService) {}

  ngOnInit(): void {
    this.loadSoftwareList();
  }

  private loadSoftwareList() {
    this.softwareService.getAllSoftware().subscribe({
      next: (data: SoftwareInterface[]) => {
        this.softwareList = data;
        console.log('Software List:', this.softwareList);
        console.log(this.softwareList[0]);
      },
      error: (error) => {
        console.error('Error loading software list:', error);
      }
    });
  }
}
