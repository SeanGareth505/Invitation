import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from './services/api.service';
@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    InputTextareaModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule 
  ],
  providers: [ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
