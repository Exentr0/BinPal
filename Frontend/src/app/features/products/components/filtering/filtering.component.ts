import {Component, Input, OnInit} from "@angular/core";
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';



@Component({
  selector: 'mc-filtering',
  templateUrl: './filtering.component.html',
  styleUrls: ['./filtering.component.css']
})

export class FilteringComponent implements OnInit {
  @Input('rangePrice') rangePrice!: number[]
  priceForm!: FormGroup;
  priceSlider: number[] = [];
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


  constructor(private fb: FormBuilder) {
  }


  ngOnInit() {
    this.initializePriceForm()
    this.selectedCategory2 = this.categories2[0];
  }


  initializePriceForm(): void {
    this.priceForm = this.fb.group({
      priceLeft: [
        this.rangePrice[0],
        [Validators.min(this.rangePrice[0]), this.maxValueValidator(this.rangePrice[1])],
      ],
      priceRight: [
        this.rangePrice[1],
        [Validators.min(this.rangePrice[0]), this.maxValueValidator(this.rangePrice[1])],
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
    this.priceSlider[0] = +event.target.value;
  }

  onInputPriceRightChange(event: any): void {
    this.priceSlider[1] = +event.target.value;
  }

  maxValueValidator(maxValue: number) {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const value = parseFloat(control.value);

      if (!isNaN(value) && value > maxValue) {
        return { 'maxValue': { 'max': maxValue, 'actual': control.value } };
      }

      return null;
    };
  }


}
