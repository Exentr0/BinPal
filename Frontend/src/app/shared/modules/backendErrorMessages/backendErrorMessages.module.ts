
import {CommonModule} from "@angular/common";
import {NgModule} from "@angular/core";
import {BackendErrorMessagesComponent} from "./components/backendErrorMessages/backendErrorMessages.component";


@NgModule({
  imports: [CommonModule],
  declarations: [BackendErrorMessagesComponent],
  exports: [BackendErrorMessagesComponent]   //дозволяє заданий компонент використовувати зовні модуля
})

export class BackendErrorMessagesModule {}
