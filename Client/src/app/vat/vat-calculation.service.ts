import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CalculatedVat } from './models/calculated-vat.model';
import {VatCalculationRequest} from './models/vat-calculation-request';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class VatCalculationService {
  private apiUrl = `${environment.apiUrl}/api/vat/calculate`;

  private readonly http = inject(HttpClient);

  public calculateVat(vatPayload: VatCalculationRequest): Observable<CalculatedVat> {
    return this.http.post<CalculatedVat>(this.apiUrl, vatPayload);
  }
}
