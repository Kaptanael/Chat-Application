import { NgModule } from '@angular/core';
import { E164PhoneNumber } from '../directives/e164-phone-number.directive';


@NgModule({
    declarations: [E164PhoneNumber],
    exports: [E164PhoneNumber]
})
export class InputBehaviorModule { }
