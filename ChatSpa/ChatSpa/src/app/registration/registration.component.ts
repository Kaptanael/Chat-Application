import { HttpErrorResponse, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthService } from '../core/services/auth.service';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
    registerFormGroup!: FormGroup;

    constructor(private fb: FormBuilder, private router: Router, private authService: AuthService, private messageService: MessageService) { }

    ngOnInit() {
        this.createRegisterForm();
    }

    createRegisterForm() {
        this.registerFormGroup = this.fb.group({
            firstName: ['', [Validators.required, this.noWhitespaceValidator, Validators.maxLength(64)]],
            lastName: ['', [Validators.required, this.noWhitespaceValidator, Validators.maxLength(64)]],
            email: ['', [Validators.required, this.noWhitespaceValidator, Validators.maxLength(64), Validators.email]]
        });
    }

    noWhitespaceValidator(control: AbstractControl) {
        if (control && control.value && !control.value.replace(/\s/g, '').length) {
            control.setValue('');
        }
        return null;
    }

    onRegister() {
        if (this.registerFormGroup.valid) {
            const userModel: any = {
                firstName: this.registerFormGroup.get('firstName')?.value,
                lastName: this.registerFormGroup.get('lastName')?.value,
                email: this.registerFormGroup.get('email')?.value
            };
            this.authService.register(userModel).subscribe((response) => {                
                if (response.status === 201) {
                    this.registerFormGroup.reset({ selfOnly: true });
                    this.messageService.add({ key: 'toastKey1', severity: 'success', summary: 'Registration successful', detail: '' });
                }

            }, (error: HttpErrorResponse) => {
                this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Failed to register', detail: '' });
            });                
        }
    }

    onEmailBlur(event: any) {
        console.log(event.target.value);
        if (event.target.value) {
            this.authService.isExistEmail(event.target.value.trim())
                .subscribe((responeData: { body: boolean; }) => {
                    if (responeData.body === true) {
                        this.registerFormGroup.controls.email.setErrors({ invalid: true });
                        this.messageService.add({ key: 'toastKey1', severity: 'success', summary: 'Email is already in use', detail: '' });                        
                    }
                },
                    (error: HttpErrorResponse) => {
                        this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Failed to check duplicate email', detail: '' });                        
                    });
        }
    }    
}
