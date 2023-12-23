import {Component, Input, OnInit} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from "@angular/router";


@Component({
  selector: 'mc-filtering',
  templateUrl: './filtering.component.html',
  styleUrls: ['./filtering.component.css']
})

export class FilteringComponent implements OnInit {
  @Input('rangePrice') rangePrice!: number[]
  priceForm!: FormGroup;
  priceSlider: number[] = [];
  priceFree: boolean = false;

  //----------------
  rating?: number
  test = 4;


  selectedCategories: any[] = [];
  categories: any[] = [
    {name: '3D', key: 'A'},
    {name: 'Audio', key: 'M'},
    {name: 'Presets', key: 'P'},
    {name: 'Templates', key: 'R'}
  ];

  selectedCategory2: any = 0;

  categories2: any[] = [
    {name: 'All', key: '0'},
    {name: '1 day ago', key: '1'},
    {name: '1 week ago', key: '2'},
    {name: '1 year ago', key: '3'}
  ];

//-----------------------
  constructor(private fb: FormBuilder, private router: Router, private route: ActivatedRoute) {
  }


  ngOnInit() {
    this.initializePriceForm()
    //-----------------------
    this.selectedCategory2 = this.categories2[0];
    //-----------------------
  }


  initializePriceForm(): void {
    this.priceForm = this.fb.group({
      priceLeft: [
        this.rangePrice[0], Validators.compose([
          Validators.required,
          Validators.min(this.rangePrice[0]),
          Validators.max(this.rangePrice[1])]),
      ],
      priceRight: [
        this.rangePrice[1], Validators.compose([
          Validators.required,
          Validators.min(this.rangePrice[0]),
          Validators.max(this.rangePrice[1])
        ])
      ],
    });

    this.priceSlider = [this.rangePrice[0], this.rangePrice[1]];

    this.priceForm.valueChanges.subscribe((values) => {
      this.priceSlider = [values.priceLeft, values.priceRight];
    });
  }

  onPriceSliderChange(event: any): void {
    this.priceForm.patchValue({
      priceLeft: event.values[0],
      priceRight: event.values[1],
    });
  }

  onInputPriceLeftChange(event: any): void {
    if (+event.target.value > this.rangePrice[1]) {
      this.priceSlider[0] = this.rangePrice[1];
    } else {
      this.priceSlider[0] = +event.target.value;
    }
  }

  onInputPriceRightChange(event: any): void {
    this.priceSlider[1] = +event.target.value;
  }

  onPriceSubmit(): void {
    if (this.priceForm.valid) {
      const priceParam =
        `${this.priceForm.controls['priceLeft'].value}-${this.priceForm.controls['priceRight'].value}`;
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: {price: priceParam},
        queryParamsHandling: "merge",
      });
    }
  }

  onInputPriceFreeChange($event: any): void {
    this.priceFree = !this.priceFree
    if (this.priceFree) {
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: {price: '0-0'},
        queryParamsHandling: "merge",
      });

    } else {
      const currentParams = {...this.route.snapshot.queryParams};
      if (currentParams.hasOwnProperty('price')) {
        delete currentParams['price'];
      }
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: currentParams
      });
    }


  }


  onInputRatingChange(event: any): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {rating: this.rating},
      queryParamsHandling: "merge",
    });
  }


}
