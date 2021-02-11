import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompareValidator } from '../directives/compare-validator.directive';


@NgModule({
    imports: [CommonModule],
    declarations: [CompareValidator],
    exports: [CompareValidator]
})
export class CompareValidatorModule { }
