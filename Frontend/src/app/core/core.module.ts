import {NgModule} from "@angular/core";
import {FooterComponent} from "./components/footer/footer.component";
import {NavbarComponent} from "./components/navbar/navbar.component";
import {RouterLink} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {AsyncPipe, NgIf} from "@angular/common";
import {MenubarModule} from "primeng/menubar";
import {InputTextModule} from "primeng/inputtext";
import {AutoFocusModule} from "primeng/autofocus";



@NgModule({
    imports: [
        RouterLink,
        FormsModule,
        AsyncPipe,
        MenubarModule,
        InputTextModule,
        AutoFocusModule,
        NgIf,
    ],
  declarations: [FooterComponent, NavbarComponent],
  exports: [FooterComponent, NavbarComponent],
  providers: []
})

export class CoreModule {
}
