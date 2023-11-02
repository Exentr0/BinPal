import {Component, OnInit} from "@angular/core";
import {FormGroup, FormBuilder,Validators} from "@angular/forms";



@Component({
  selector: 'mc-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})


export class RegisterComponent implements OnInit {
  form!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initializeForm()
  }

  initializeForm(): void {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ''
    })
    console.log(this.form.valid);
  }

  onSubmit(): void {
    console.log(this.form.value)
  }

}

