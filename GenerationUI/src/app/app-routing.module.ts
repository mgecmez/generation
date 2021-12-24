import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PowerplantComponent } from './powerplant/powerplant.component';

const routes: Routes = [
  { path: 'powerplant', component: PowerplantComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
