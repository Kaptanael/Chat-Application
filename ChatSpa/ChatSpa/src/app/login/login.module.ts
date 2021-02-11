import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { LoginComponent } from './login.component';
import { LoginRoutingModule } from './login-routing.module';
import { MessageService } from 'primeng/api';
import { AuthService } from '../core/services/auth.service';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ToastModule,
    LoginRoutingModule
  ],
    providers: [MessageService, AuthService]
  
})
export class LoginModule { }
