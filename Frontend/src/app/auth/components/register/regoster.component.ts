import {Component, OnInit} from "@angular/core";
import {FormGroup, FormBuilder,Validators} from "@angular/forms";
import {select, Store} from "@ngrx/store";
import {registerAction} from "../../store/actions/register.action";
import {Observable} from "rxjs/internal/Observable";
import {isSubmittingSelector} from "../../store/selectors";
import {AuthServise} from "../../services/auth.servise";
import {CurrentUserInterface} from "../../../shared/types/currentUser.interface";
import {RegisterRequestInterface} from "../../types/registerRequest.interface";



@Component({
  selector: 'mc-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})


export class RegisterComponent implements OnInit {
  form!: FormGroup;
  isSubmitting$!: Observable<boolean>    //дивиться за змінами (всі "стостерігачі" в кінці $)

  constructor(private fb: FormBuilder, private store: Store, private authServise: AuthServise) {}


  ngOnInit(): void {
    this.initializeForm()
    this.initializeValues()
  }


  //виключає кнопку sign in
  initializeValues(): void{
    this.isSubmitting$ = this.store.pipe(select(isSubmittingSelector)) //pipe - групування операцій
  }


  initializeForm(): void {
    this.form = this.fb.group({
      username: ['', Validators.required],
      email: '',
      password: ''
    })
  }

  onSubmit(): void {
    const request: RegisterRequestInterface = {
      user: this.form.value
    }
    this.store.dispatch(registerAction({request}))
  }

}

