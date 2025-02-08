import {patchState, signalStore, withMethods, withState} from '@ngrx/signals';
import {VatCalculationService} from './vat-calculation.service';
import {inject} from '@angular/core';
import {rxMethod} from '@ngrx/signals/rxjs-interop';
import {exhaustMap, pipe, tap} from 'rxjs';
import {CalculatedVat} from './models/calculated-vat.model';
import {tapResponse} from '@ngrx/operators';
import {VatCalculationRequest} from './models/vat-calculation-request';


type VatCalculationState = {
  calculatedVat?: CalculatedVat,
  isLoading: boolean;
  error: string;
};

const initialState: VatCalculationState = {
  calculatedVat: undefined,
  isLoading: false,
  error: '',
}

export const VatCalculationStore = signalStore(
  withState(initialState),
  withMethods((store, vatCalculationService = inject(VatCalculationService)) => ({
    calculateVat: rxMethod<VatCalculationRequest>(
      pipe(
        tap(() => patchState(store, {isLoading: true})),
        exhaustMap((vat) => {
          return vatCalculationService.calculateVat(vat).pipe(
            tapResponse({
              next: (calculatedVat) => patchState(store, {calculatedVat, isLoading: false}),
              error: () => patchState(store, {error: 'Error loading cities', isLoading: false}),
            })
          );
        })
      )
    ),
  }))
);
