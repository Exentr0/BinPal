import {Component, Input, OnInit} from "@angular/core";
import {SelectItem} from "primeng/api";
import {DropdownChangeEvent} from "primeng/dropdown";
import {ActivatedRoute, Router} from "@angular/router";




@Component({
    selector: 'mc-top',
    templateUrl: './top.component.html',
    styleUrls: ['./top.component.css']
})

export class TopComponent implements OnInit {
    @Input('totalProducts') totalProducts!: number
    @Input('limitProducts') limitProducts!: number
    @Input('currentPage') currentPage!: number

    firstProductIndex: number = 0
    lastProductIndex: number = 0
    limits: number[] = [24, 48, 72, 96];

    sortOptions!: SelectItem[];
    sortKey: any;


    constructor(private router: Router, private route: ActivatedRoute) {
    }

    ngOnInit() {
        this.calculateProductIndexes()
        this.sortDropdown()
        this.limitsDropdown()
    }


    sortDropdown(): void {
        this.sortOptions = [
            {label: 'Price High to Low', value: '!price'},
            {label: 'Price Low to High', value: 'price'}
        ];
    }

    limitsDropdown(): void {

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


    sortDropdownChange($event: DropdownChangeEvent) {
        this.router.navigate([], {
            relativeTo: this.route,
            queryParams: {limit: this.limitProducts},
            queryParamsHandling: "merge",
        });
    }


    onSortChange($event: DropdownChangeEvent) {

    }
}
