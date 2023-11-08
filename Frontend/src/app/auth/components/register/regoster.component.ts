import {Component, OnInit} from "@angular/core";
import {select, Store} from "@ngrx/store";
import {FormGroup, FormBuilder, Validators} from "@angular/forms";
import {Observable} from "rxjs/internal/Observable";
import {isSubmittingSelector, validationErrorsSelector} from "src/app/auth/store/selectors";
import {registerAction} from "src/app/auth/store/actions/register.action";
import {RegisterRequestInterface} from "src/app/auth/types/registerRequest.interface";
import {BackendErrorsInterface} from "src/app/shared/types/backendErrors.interface";


@Component({
  selector: 'mc-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})


export class RegisterComponent implements OnInit {
  form!: FormGroup;
  isSubmitting$!: Observable<boolean>    // Observable дивиться за змінами, тупу підписується на зміни (всі "стостерігачі" в кінці $)
  backendErrors$!: Observable<BackendErrorsInterface | null>

  constructor(private fb: FormBuilder, private store: Store) {
  }


  ngOnInit(): void {
    this.initializeValues()
    this.initializeForm()
  }


  //задаємо "спостерігачів" через селектори
  initializeValues(): void {
    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector)) //pipe - групування операцій
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector))
  }


  initializeForm(): void {
    this.form = this.fb.group({
      username: ['', Validators.required],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(8)])],
    })
  }


  onSubmit(): void {
    if (this.form.valid) {
      const request: RegisterRequestInterface = {user: this.form.value}  //отримуємо значення з форми
      this.store.dispatch(registerAction({request})) //відправляємо
    }
  }

}

