import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { SoftwareInterface } from "../../../shared/types/software.interface";
import {select, Store} from "@ngrx/store";
import {getSoftwareAction} from "./store/actions/getSoftware.action";
import {errorSelector, isLoadingSelector, softwareSuccessSelector} from "./store/selectors";
import {Observable} from "rxjs/internal/Observable";

@Component({
  selector: 'app-software-selector-component',
  templateUrl: './software-selector-compoent.component.html',
  styleUrls: ['./software-selector-compoent.component.css']
})
export class SoftwareSelectorComponent implements OnInit {

  @Input() selectedSoftwareList: SoftwareInterface[] = [];

  @Output() selectionChange = new EventEmitter<SoftwareInterface[]>();

  isLoading$ : Observable<boolean>;

  error$ : Observable<string | null>;

  softwareOptions$ : Observable<SoftwareInterface[]>;

  constructor(private store: Store) {
    this.isLoading$ = this.store.pipe(select(isLoadingSelector));
    this.error$ = this.store.pipe(select(errorSelector));
    this.softwareOptions$ = this.store.pipe(select(softwareSuccessSelector))
  }

  ngOnInit(): void {
    this.store.dispatch(getSoftwareAction());
  }

  isSelected(software: SoftwareInterface): boolean {
    return this.selectedSoftwareList?.some(s => s.id === software.id);
  }

  selectSoftware(software: SoftwareInterface): void {
    const index = this.selectedSoftwareList.findIndex(s => s.id === software.id);

    if (index !== -1) {
      this.selectedSoftwareList.splice(index, 1);
    } else {
      this.selectedSoftwareList.push(software);
    }

    this.selectionChange.emit(this.selectedSoftwareList);
  }
}
