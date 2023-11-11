import {Component, Input} from "@angular/core";

@Component({
  selector: 'mc-error-message',
  templateUrl: './errorMessage.component.html',
  styleUrls: ['./errorMessage.component.css']
})

export class ErrorMessageComponent{
  @Input('message') messageProps: string = 'Something went wrong'
}
