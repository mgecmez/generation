import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PowerplantComponent } from './powerplant/powerplant.component';
import { DetailPowerplantComponent } from './powerplant/detail-powerplant/detail-powerplant.component';
import { AddEditPowerplantComponent } from './powerplant/add-edit-powerplant/add-edit-powerplant.component';

import { HttpClientModule } from "@angular/common/http";
import { SharedService } from "./services/shared.service";
import { FormsModule,ReactiveFormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    PowerplantComponent,
    DetailPowerplantComponent,
    AddEditPowerplantComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [SharedService],
  bootstrap: [AppComponent]
})
export class AppModule { }
