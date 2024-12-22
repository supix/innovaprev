import {AbstractControl, ValidationErrors, ValidatorFn} from '@angular/forms';

// Checks if the value meets the minimum requirement or returns a validation error.
export function minNumber(min: number, required: boolean = false, gender: 'm' | 'f' = 'm'): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    // If the value is null, undefined, or an empty string
    if (value === null || value === undefined || value === '') {
      if (required) {
        return {invalidValue: {reason: ` è obbligatori${gender === 'm' ? 'o' : 'a'}`}};
      }
      return null; // Not required and empty values are allowed
    }

    // Ensure the value is numeric
    const numericValue = Number(value);
    if (isNaN(numericValue)) {
      return {invalidValue: {reason: ' deve essere un numero valido'}};
    }

    // Check if the value meets the minimum requirement
    if (numericValue < min) {
      return {invalidValue: {reason: ` è minimo di ${min}`}};
    }

    return null; // Validation passed
  };
}

// Checks the validity of the Italian Vat or returns a validation error.
export function italianVatValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    // Null or empty values are not validated
    if (value === null || value === undefined || value === '') {
      return {italianVat: {reason: 'La partita IVA è obbligatoria.'}};
    }

    // Ensure the value is a string of exactly 11 numeric characters
    if (!/^\d{11}$/.test(value)) {
      return {italianVat: {reason: 'La partita IVA deve essere di 11 cifre.'}};
    }

    // Validate the Italian Vat using the checksum algorithm
    if (!isValidItalianVat(value)) {
      return {italianVat: {reason: 'La partita IVA non è valida.'}};
    }

    return null;
  };
}

// Checks if the Italian Vat is valid using the checksum algorithm.
export function isValidItalianVat(vat: string): boolean {
  const digits = vat.split('').map(Number);
  let sum = 0;

  for (let i = 0; i < 11; i++) {
    if (i % 2 === 0) {
      // Even positions (0-based index): add the digit as is
      sum += digits[i];
    } else {
      // Odd positions: double the digit and subtract 9 if the result is >= 10
      const doubled = digits[i] * 2;
      sum += doubled > 9 ? doubled - 9 : doubled;
    }
  }

  // The sum must be divisible by 10
  return sum % 10 === 0;
}

// Validator for phone numbers
export function phoneNumberValidator(required: boolean = false, minLength: number = 8, maxLength: number = 15): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;

    // If the value is null, undefined, or an empty string
    if (value === null || value === undefined || value === '') {
      if (required) {
        return {invalidPhoneNumber: {reason: 'Il numero di telefono è obbligatorio'}};
      }
      return null; // Not required and empty values are allowed
    }

    // Remove spaces and dashes to validate only numeric characters
    const sanitizedValue = value.replace(/[\s\-]/g, '');

    // Check if the value contains only numbers
    if (!/^\d+$/.test(sanitizedValue)) {
      return {invalidPhoneNumber: {reason: 'Il numero di telefono deve contenere solo cifre numeriche.'}};
    }

    // Check the length requirements
    if (sanitizedValue.length < minLength || sanitizedValue.length > maxLength) {
      return {
        invalidPhoneNumber: {
          reason: `Il numero di telefono è troppo breve.`,
        },
      };
    }

    return null; // Validation passed
  };
}
