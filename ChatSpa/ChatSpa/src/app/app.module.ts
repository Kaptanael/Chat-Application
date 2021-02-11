import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from "@angular/core";
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from "./app-routing.module";
import { CoreModule } from './core/core.module';

import { AppComponent } from "./app.component";
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [AppComponent, HomeComponent ],
  imports: [BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    CoreModule    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
