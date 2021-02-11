import { NgModule } from '@angular/core';
import { OnlyNumber } from '../directives/only-number.directive';


@NgModule({
    declarations: [OnlyNumber],
    exports: [OnlyNumber]
})
export class OnlyNumberModule { }
