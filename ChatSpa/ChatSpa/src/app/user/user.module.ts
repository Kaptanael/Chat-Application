import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { NgxSpinnerModule } from 'ngx-spinner';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { MessageService } from 'primeng/api';
import { UserService } from '../_services/user.service';
import { MessageHubService } from '../_services/message-hub.service';

@NgModule({
  declarations: [UserComponent],
  imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      ToastModule,
      DropdownModule,
      TableModule,
      DialogModule,
      NgxSpinnerModule,
      UserRoutingModule
    ],
    providers: [MessageService, UserService, MessageHubService]
})
export class UserModule { }
