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
  testa = [4, 100];

  selectedCategories: any[] = [];
  categories: any[] = [
    {name: 'Test', key: 'A'},
    {name: 'Test1', key: 'M'},
    {name: 'Test2', key: 'P'},
    {name: 'Test3', key: 'R'}
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

    // Initialize priceSlider with default values
    this.priceSlider = [this.rangePrice[0], this.rangePrice[1]];

    // Subscribe to form control changes to update priceSlider
    this.priceForm.valueChanges.subscribe((values) => {
      this.priceSlider = [values.priceLeft, values.priceRight];
    });
  }

  onPriceSliderChange(event: any): void {
    // Update form values when slider changes
    this.priceForm.patchValue({
      priceLeft: event.values[0],
      priceRight: event.values[1],
    });
  }

  onInputPriceLeftChange(event: any): void {
    // Update slider value when left input changes
    this.priceSlider[0] = +event.target.value;
  }

  onInputPriceRightChange(event: any): void {
    // Update slider value when right input changes
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
