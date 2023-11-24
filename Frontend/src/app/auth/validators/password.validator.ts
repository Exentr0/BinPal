import {AbstractControl, ValidatorFn} from "@angular/forms";

export function customPasswordValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: boolean } | null => {
    const value: string = control.value;

    // Перевірка наявності хоча б одної великої літери
    const hasUpperCase = /[A-Z]/.test(value);

    // Перевірка наявності хоча б одної малої літери
    const hasLowerCase = /[a-z]/.test(value);

    // Якщо є хоча б одна велика і одна мала літера, валідація пройшла успішно
    if (hasUpperCase && hasLowerCase) {
      return null;
    } else {
      // Якщо не виконується умова, повертаємо помилку
      return { 'customPassword': true };
    }
  };
}
