import {Component, Input, OnInit} from "@angular/core";


@Component({
  selector: 'mc-filtering',
  templateUrl: './filtering.component.html',
  styleUrls: ['./filtering.component.css']
})

export class FilteringComponent implements OnInit{
  @Input('rangePrice') rangePrice!: number[]
  rating?: number

  selectedCategories: any[] = [];
  categories: any[] = [
    { name: 'Test', key: 'A' },
    { name: 'Test1', key: 'M' },
    { name: 'Test2', key: 'P' },
    { name: 'Test3', key: 'R' }
  ];

  selectedCategory2: any = 0;

  categories2: any[] = [
    { name: 'All', key: '0' },
    { name: '1 day ago', key: '1' },
    { name: '1 week ago', key: '2' },
    { name: '1 year ago', key: '3' }
  ];




  ngOnInit() {
    this.selectedCategory2 = this.categories2[0];
  }
}
