import {Component, Input, OnChanges, OnInit, SimpleChanges} from "@angular/core";
import {DropdownChangeEvent} from "primeng/dropdown";
import {ActivatedRoute, Router} from "@angular/router";

interface Sorting {
  name: string;
  code: number;
}


@Component({
  selector: 'mc-top',
  templateUrl: './top.component.html',
  styleUrls: ['./top.component.css']
})

export class TopComponent implements OnInit, OnChanges  {
  @Input('totalProducts') totalProducts!: number
  @Input('limitProducts') limitProducts!: number
  @Input('currentPage') currentPage!: number
  @Input('selectedSorting') sortingCode!: number

  firstProductIndex: number = 0
  lastProductIndex: number = 0
  limits!: number[];
  sortingOptions!: Sorting[];
  selectedSorting!: Sorting;


  constructor(private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.calculateProductIndexes()
    this.sortingDropdown()
    this.limitsDropdown()
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes["totalProducts"] && !changes["totalProducts"].firstChange) {
      this.calculateProductIndexes();
    }
  }


  sortingDropdown(): void {
    this.sortingOptions = [
      {name: 'Price High to Low', code: 1},
      {name: 'Price Low to High', code: 2}
    ];

    this.selectedSorting = this.sortingOptions.find(
        option => option.code === this.sortingCode)
      || this.sortingOptions[0];
  }

  sortingChange($event: DropdownChangeEvent) {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {sorting: this.selectedSorting.code},
      queryParamsHandling: "merge",
    });
  }


  limitsDropdown(): void {
    this.limits = [24, 48, 72, 96];
  }


  limitDropdownChange($event: DropdownChangeEvent) {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {limit: this.limitProducts},
      queryParamsHandling: "merge",
    });
    this.calculateProductIndexes()
  }


  calculateProductIndexes(): void {
    this.firstProductIndex = (this.currentPage - 1) * this.limitProducts;
    this.lastProductIndex = this.firstProductIndex + this.limitProducts;
    if (this.totalProducts) {
      if (this.lastProductIndex > this.totalProducts) {
        this.lastProductIndex = this.totalProducts
      }
    }

  }


}
