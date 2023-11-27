import {Component, Input, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {PaginatorState} from "primeng/paginator";

@Component({
  selector: 'mc-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})


export class PaginationComponent implements OnInit {
  @Input('totalProducts') totalProducts!: number
  @Input('limitProducts') limitProducts!: number
  @Input('currentPage') currentPage!: number


  first!: number


  constructor(private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.first = this.currentPage * this.limitProducts - this.limitProducts
  }


  onPageChange(event: PaginatorState) {
    this.first = event.first || 0;
    this.limitProducts = event.rows || 24;

    this.currentPage = (this.first + this.limitProducts) / this.limitProducts;

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {page: this.currentPage, limit: this.limitProducts},
      queryParamsHandling: "merge",
    });

  }


}
