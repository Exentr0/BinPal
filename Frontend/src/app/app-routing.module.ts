import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';


const routes: Routes = [
  {
    path: 'add-package',
    loadChildren: () =>
      import('./features/package-adding/package-adding.module').then(
        (m) => m.PackageAddingModule
      ),
  },
];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
