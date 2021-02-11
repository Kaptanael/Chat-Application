import { Directive, ElementRef, HostListener, Input } from '@angular/core';
import { NgControl, FormControl } from '@angular/forms';

@Directive({
    selector: '[E164PhoneNumber], [formControl], [formControlName]'
})
export class E164PhoneNumber {
    private previousValue: string;
    constructor(
        private controlDir: NgControl,private el: ElementRef) { }
    @Input() E164PhoneNumber: boolean;
    @HostListener('paste', ['$event']) onPaste(event) {
        if (this.E164PhoneNumber) {
            if (this.el.nativeElement.value) {
                this.previousValue = this.el.nativeElement.value.trim();
            }
            else {
                this.previousValue = null;
            }
            //console.log(event);
            setTimeout(() => {
                if (!/^[\+\d]?(?:[\d\s]*)$/.test(this.el.nativeElement.value)) {
                    //event.cancel;
                    if (this.previousValue) {
                        this.el.nativeElement.value = this.previousValue;
                        this.controlDir.control.setValue(this.previousValue);
                        this.controlDir.control.updateValueAndValidity();
                        //console.log(this.controlDir);
                        //console.log(this.el);
                    }
                    else {
                        this.previousValue = null;
                        this.el.nativeElement.value = null;
                        //this.el.nativeElement.change();
                        //let e = this.c.root.get(this.el.nativeElement);
                        //var modelController = this.el.nativeElement.find('input').controller('ngModel');
                        //console.log(this.el);
                        //console.log(this.controlDir);
                        //console.log(this.el.nativeElement);
                        //let fromControl = new FormControl();
                        //let validationResult = this.controlDir.control.validator(fromControl)
                        //if (validationResult !== null && validationResult.required === true) {
                            this.controlDir.control.setErrors({ 'required': true });
                        //}
                        //console.log(this.el.nativeElement.value + 'In Else');
                    }
                }
                
            });
        }
    }

    @HostListener('keydown', ['$event']) onKeyDown(event) {
        let e = <KeyboardEvent>event;
        //console.log(e.keyCode);
        if (this.E164PhoneNumber) {
            if ([46, 8, 9, 27, 13].indexOf(e.keyCode) !== -1 ||
                // Allow: Ctrl+A
                (e.keyCode == 65 && e.ctrlKey === true) ||
                // Allow: Ctrl+C
                (e.keyCode == 67 && e.ctrlKey === true) ||
                // Allow: Ctrl+V
                (e.keyCode == 86 && e.ctrlKey === true) ||
                // Allow: Ctrl+X
                (e.keyCode == 88 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105) && e.keyCode !== 107 && e.keyCode !== 187) {

                //console.log('Key block');
                e.preventDefault();
            }
            //console.log(this.el.nativeElement.value);
            if (!/^[\+\d]?(?:[\d\s]*)$/.test(this.el.nativeElement.value)) {
                //console.log('Pattern block');
                this.el.nativeElement.value = this.el.nativeElement.value.substr(0, this.el.nativeElement.value.length - 1);

            }
        }        
    }

    @HostListener('oninput', ['$event']) onInput(event) {
        let e = <KeyboardEvent>event;
        //console.log(e.keyCode);
        if (this.E164PhoneNumber) {
            if ([46, 8, 9, 27, 13].indexOf(e.keyCode) !== -1 ||
                // Allow: Ctrl+A
                (e.keyCode == 65 && e.ctrlKey === true) ||
                // Allow: Ctrl+C
                (e.keyCode == 67 && e.ctrlKey === true) ||
                // Allow: Ctrl+V
                (e.keyCode == 86 && e.ctrlKey === true) ||
                // Allow: Ctrl+X
                (e.keyCode == 88 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105) && e.keyCode !== 107 && e.keyCode !== 187) {

                //console.log('Key block');
                e.preventDefault();
            }
            //console.log(this.el.nativeElement.value);
            if (!/^[\+\d]?(?:[\d\s]*)$/.test(this.el.nativeElement.value)) {
                //console.log('Pattern block');
                this.el.nativeElement.value = this.el.nativeElement.value.substr(0, this.el.nativeElement.value.length - 1);

            }
        }
    }

    @HostListener('keyup', ['$event']) onKeyUp(event) {
        let e = <KeyboardEvent>event;
        if (this.E164PhoneNumber) {
            if ([46, 8, 9, 27, 13].indexOf(e.keyCode) !== -1 ||
                // Allow: Ctrl+A
                (e.keyCode == 65 && e.ctrlKey === true) ||
                // Allow: Ctrl+C
                (e.keyCode == 67 && e.ctrlKey === true) ||
                // Allow: Ctrl+V
                (e.keyCode == 86 && e.ctrlKey === true) ||
                // Allow: Ctrl+X
                (e.keyCode == 88 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105) && e.keyCode !== 107 && e.keyCode !== 187) {

                //console.log('Key block');
                e.preventDefault();
            }
            //console.log(this.el.nativeElement.value);
            if (!/^[\+\d]?(?:[\d\s]*)$/.test(this.el.nativeElement.value)) {
                //console.log('Pattern block');
                this.el.nativeElement.value = this.el.nativeElement.value.substr(0, this.el.nativeElement.value.length - 1);

            }
        }
    }
}
