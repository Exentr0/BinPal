import { Component } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { PackageAddingService } from "../services/packageAddingService";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-main-window-component',
  templateUrl: './main-window-component.component.html',
  styleUrls: ['./main-window-component.component.css']
})
export class MainWindowComponentComponent {
  items: MenuItem[] = [];

  subscription: Subscription = new Subscription();

  constructor(public messageService: MessageService, public ticketService: PackageAddingService) {}

  ngOnInit() {
    this.items = [
      {
        label: 'General',
        routerLink: 'general',
      },
      {
        label: 'Supported Apps',
        routerLink: 'supported-software'
      },
      {
        label: 'Required Plugins',
        routerLink : 'required-plugins',
      },
      {
        label: 'Categories',
        routerLink: 'categories'
      },
      {
        label: 'Media',
        routerLink: 'media'
      },
      {
        label: 'Content',
        routerLink: 'content'
      }
    ];

    this.subscription = this.ticketService.packageUploaded$.subscribe((generalInformation) => {
      this.messageService.add({ severity: 'success', summary: generalInformation.name + ' was Uploaded', detail: 'Your package was Added.' });
    });
  }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }
}