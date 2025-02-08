import {Component, inject} from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  Validators
} from '@angular/forms';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {CommonModule} from '@angular/common';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import {VatCalculationService} from '../vat-calculation.service';
import {VatCalculationStore} from '../vat-calculation.store';
import {VatCalculationRequest} from '../models/vat-calculation-request';

@Component({
  selector: 'app-vat-calculator',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
  ],
  templateUrl: './vat-calculator.component.html',
  styleUrls: ['./vat-calculator.component.scss'],
  providers: [VatCalculationStore],
})
export class VatCalculatorComponent {
  private readonly fb = inject(FormBuilder);
  private readonly snackBar = inject(MatSnackBar);
  private readonly store = inject(VatCalculationStore);

  readonly calculatedVat = this.store.calculatedVat;

  vatForm = this.fb.group({
    vatRate: ['', [Validators.required, Validators.pattern(/^(10|13|20)$/)]],
    netAmount: ['', [Validators.min(0)]],
    grossAmount: ['', [Validators.min(0)]],
    vatAmount: ['', [Validators.min(0)]],
  }, {validators: this.validateAmountFields});

  onSubmit() {
    this.store.calculateVat(this.getFormValues());
  }

  private validateAmountFields(group: AbstractControl): ValidationErrors | null {
    const formGroup = group as FormGroup;
    const {netAmount, grossAmount, vatAmount} = formGroup.controls;

    const filledFields = [netAmount, grossAmount, vatAmount].filter(control => control.value !== '' && control.value !== null && control.value !== undefined);

    if (filledFields.length !== 1) {
      return {invalidAmountFields: true};
    }

    return null;
  }

  private getFormValues(): VatCalculationRequest {
    const {vatRate, netAmount, grossAmount, vatAmount} = this.vatForm.value;
    return {
      vatRate: Number(vatRate),
      netAmount: netAmount ? Number(netAmount) : undefined,
      grossAmount: grossAmount ? Number(grossAmount) : undefined,
      vatAmount: vatAmount? Number(vatAmount) : undefined,
    };
  }
}
