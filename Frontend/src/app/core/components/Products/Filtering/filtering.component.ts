import {Component, Input, OnInit} from "@angular/core";


@Component({
  selector: 'mc-filtering',
  templateUrl: './filtering.component.html',
  styleUrls: ['./filtering.component.css']
})

export class FilteringComponent implements OnInit{
  @Input('rangePrice') rangePrice!: number[]

  ngOnInit() {
  }
}
