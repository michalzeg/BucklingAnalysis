import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Facade } from '../shared/models/facade';
import { HttpClient } from '@angular/common/http';
import { CalculationResults } from '../shared/models/results/calculation-results';

const url = `${environment.baseUrl}/api/Storage`;

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private readonly client: HttpClient) { }

  getFacadeDetails(trackingNumber: string): Observable<Facade> {
    return this.client.get<Facade>(`${url}/${trackingNumber}/FacadeDetails`)
  }

  getLinearAnalysisResults(trackingNumber: string): Observable<CalculationResults> {
    return this.client.get<CalculationResults>(`${url}/${trackingNumber}/FilteredLinearAnalysisResults`)
  }

  getBucklingAnalysisResults(trackingNumber: string): Observable<CalculationResults> {
    return this.client.get<CalculationResults>(`${url}/${trackingNumber}/BucklingAnalysisResults`)
  }

  getNonLinearAnalysisResults(trackingNumber: string): Observable<CalculationResults> {
    return this.client.get<CalculationResults>(`${url}/${trackingNumber}/FilteredNonLinearAnalysisResults`)
  }

}
