import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { RegistrationComponent } from './registration.component';
import { RegistrationRoutingModule } from './registration-routing.module';
import { MessageService } from 'primeng/api';
import { AuthService } from '../core/services/auth.service';

@NgModule({
    declarations: [RegistrationComponent],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ToastModule,
        RegistrationRoutingModule
    ],
    providers: [MessageService, AuthService]

})
export class RegistrationModule { }
