import {Component, Input, OnInit} from "@angular/core";
import {BackendErrorsInterface} from "src/app/shared/types/backendErrors.interface";


@Component({
    selector: 'mc-backend-error-messages',
    templateUrl: './backendErrorMessages.component.html',
    styleUrls: ['./backendErrorMessages.component.css']
})

export class BackendErrorMessagesComponent implements OnInit{
    @Input('backendErrors') backendErrorsProps!: BackendErrorsInterface | null

errorMessages!: string[]

    ngOnInit() {
        if (this.backendErrorsProps !== null) {
            this.errorMessages = Object.keys(this.backendErrorsProps).map((name: string) => {
                const messages = this.backendErrorsProps![name].join(', '); // Використовуємо non-null assertion operator
                return `${messages}`;
                // return `${name} ${messages}`;
            });
        } else {
            this.errorMessages = []; // Обробка випадку, коли this.backendErrorsProps === null
        }
    }


}
