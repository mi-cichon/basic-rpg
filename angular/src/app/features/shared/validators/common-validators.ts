import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function confirmPasswordValidator(
  passwordControlName: string,
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const passwordControl = control.parent?.get(passwordControlName);
    return passwordControl?.value !== control.value
      ? { passwordsNotSame: { message: "Password are not the same" } }
      : null;
  };
}
