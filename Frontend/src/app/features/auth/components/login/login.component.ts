import {Component, OnInit} from "@angular/core";
import {select, Store} from "@ngrx/store";
import {FormGroup, FormBuilder, Validators} from "@angular/forms";
import {Observable} from "rxjs/internal/Observable";
import {BackendErrorsInterface} from "src/app/shared/types/backendErrors.interface";
import {LoginRequestInterface} from "../../types/loginRequest.interface";
import {loginAction} from "../../store/actions/login.action";
import {customPasswordValidator} from "../../validators/password.validator";
import {isSubmittingSelector, validationErrorsSelector} from "../../store/selectors";



@Component({
  selector: 'mc-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})


export class LoginComponent implements OnInit {
  form!: FormGroup;
  isSubmitting$!: Observable<boolean>
  backendErrors$!: Observable<BackendErrorsInterface | null>

  constructor(private fb: FormBuilder, private store: Store) {
  }


  ngOnInit(): void {
    this.initializeForm()
    this.initializeValues()
  }


  initializeValues(): void {
    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector))
    this.backendErrors$ = this.store.pipe(select(validationErrorsSelector))
  }


  initializeForm(): void {
    this.form = this.fb.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(8), customPasswordValidator()])],
    })
  }


  onSubmit(): void {
    if (this.form.valid) {
      const request: LoginRequestInterface = this.form.value  //отримуємо значення з форми
      this.store.dispatch(loginAction({request})) //відправляємо
    }
  }

}

