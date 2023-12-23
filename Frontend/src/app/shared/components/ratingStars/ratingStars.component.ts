import {Component, Input, OnInit} from "@angular/core";

@Component({
  selector: 'mc-ratingStars',
  templateUrl: './ratingStars.component.html',
  styleUrls: ['./ratingStars.component.css']
})

export class RatingStarsComponent implements OnInit {
  @Input('rating') rating!: number
  fullStars!: number
  emptyStars!: number

  ngOnInit() {
    // this.fullStars = Math.ceil(this.rating);
    this.fullStars = Math.floor(this.rating);
    this.emptyStars = 5 - this.fullStars
  }


  getRange(value: number): number[] {
    return Array.from({ length: value });
  }

}
