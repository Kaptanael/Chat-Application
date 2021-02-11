import { Directive, forwardRef, Attribute, Input } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';
import { isNullOrUndefined } from 'util';

@Directive({
    selector: '[compare][formControlName],[compare][formControl],[compare][ngModel]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => CompareValidator), multi: true }
    ]
})
export class CompareValidator implements Validator {

    constructor(@Attribute('validateEqual') public compare: string,
        @Attribute("compare-operator") public compareoperator: string,
        @Attribute('reverse') public reverse: string) {
    }

    private get isReverse() {
        if (!this.reverse) return false;
        return this.reverse === 'true' ? true : false;
    }

    validate(c: AbstractControl): { [key: string]: any } {
        // self value
        let v = c.value;

        // control vlaue
        let e = c.root.get(this.compare);

        //console.log(v);
        if (!isNullOrUndefined(e) && !isNullOrUndefined(e.value)) {
            //console.log(e.value);
            //console.log(e.value.toLocaleString().length);

        }
        if (!isNullOrUndefined(e) && !isNullOrUndefined(e.value) && e.value.toLocaleString().length > 0 && !isNullOrUndefined(v) && v.toLocaleString().length > 0) {
            //console.log(e);
            //console.log(v);
            //console.log(!isNullOrUndefined(e.errors));
            if (this.compareoperator === '=') {
                // value not equal
                if (e && v !== e.value && !this.isReverse) {
                    return {
                        validateEqual: false
                    }
                }

                // value equal and reverse
                if (e && v === e.value && this.isReverse) {
                    if (!isNullOrUndefined(e.errors)) {
                        delete e.errors['validateEqual'];
                        if (!Object.keys(e.errors).length) { e.setErrors(null); }
                    }
                }

                // value not equal and reverse
                if (e && v !== e.value && this.isReverse) {
                    e.setErrors({ validateEqual: false });
                    //return {
                    //    validateEqual: false
                    //}
                }
            }
            else if (this.compareoperator === '>') {
                // value not equal
                if (e && v <= e.value && !this.isReverse) {
                    return {
                        validateEqual: false
                    }
                }

                // value equal and reverse
                if (e && v > e.value && this.isReverse) {
                    if (!isNullOrUndefined(e.errors)) {
                        delete e.errors['validateEqual'];
                        if (!Object.keys(e.errors).length) { e.setErrors(null); }
                    }
                }

                // value not equal and reverse
                if (e && v <= e.value && this.isReverse) {
                    e.setErrors({ validateEqual: false });
                    //return {
                    //    validateEqual: false
                    //}
                }
            }
            else if (this.compareoperator === '<') {
                //console.log(e);
                //console.log(v);
                // value not equal
                if (e && v >= e.value && !this.isReverse) {
                    return {
                        validateEqual: false
                    }
                }

                // value equal and reverse
                if (e && v < e.value && this.isReverse) {
                    if (!isNullOrUndefined(e.errors)) {
                        delete e.errors['validateEqual'];
                        if (!Object.keys(e.errors).length) { e.setErrors(null); }
                    }
                }

                // value not equal and reverse
                if (e && v >= e.value && this.isReverse) {
                    e.setErrors({ validateEqual: false });
                    //return {
                    //    validateEqual: false
                    //}
                }
            }
        }
        return null;
    }
}
