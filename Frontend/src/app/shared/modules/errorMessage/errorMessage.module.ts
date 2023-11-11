import {CommonModule} from "@angular/common";
import {ErrorMessageComponent} from "./components/errorMessage/errorMessage.component";
import {NgModule} from "@angular/core";

@NgModule ({
  imports: [CommonModule],
  declarations: [ErrorMessageComponent],
  exports: [ErrorMessageComponent]
})

export class ErrorMessageModule{}
