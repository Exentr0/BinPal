import {Component, OnInit} from "@angular/core";
import {select, Store} from "@ngrx/store";
import {FormGroup, FormBuilder, Validators} from "@angular/forms";
import {Observable} from "rxjs/internal/Observable";

import {BackendErrorsInterface} from "src/app/shared/types/backendErrors.interface";
import {customPasswordValidator} from "../../validators/password.validator";
import {isSubmittingSelector, validationErrorsSelector} from "../../store/selectors";
import {RegisterRequestInterface} from "../../types/registerRequest.interface";
import {registerAction} from "../../store/actions/register.action";

@Component({
  selector: 'mc-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})


export class RegisterComponent implements OnInit {
  form!: FormGroup;
  isSubmitting$!: Observable<boolean>
  backendErrors$!: Observable<BackendErrorsInterface | null>

  constructor(private fb: FormBuilder, private store: Store) {
  }


  ngOnInit(): void {
    this.initializeValues()
    this.initializeForm()
  }


  initializeValues(): void {
    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector))
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector))
  }


  initializeForm(): void {
    this.form = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(8), customPasswordValidator()])],
    })
  }


  onSubmit(): void {
    if (this.form.valid) {
      const request: RegisterRequestInterface = this.form.value  //отримуємо значення з форми
      this.store.dispatch(registerAction({request})) //відправляємо
    }
  }




}


