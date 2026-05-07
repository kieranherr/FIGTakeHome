import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AdvisorSummariesResponse, CarrierDistribution, Statement } from '../models/advisor.model';

@Injectable({ providedIn: 'root' })
export class AdvisorService {
  private readonly baseUrl = 'http://localhost:5122/advisor';

  constructor(private http: HttpClient) {}

  getSummaries(): Observable<AdvisorSummariesResponse> {
    return this.http.get<AdvisorSummariesResponse>(`${this.baseUrl}/summaries`);
  }

  getCarrierDistribution(advisorId: number): Observable<CarrierDistribution> {
    return this.http.get<CarrierDistribution>(`${this.baseUrl}/${advisorId}/carrier-distribution`);
  }

  getMonthlyTrend(advisorId: number): Observable<Statement[]> {
    return this.http.get<Statement[]>(`${this.baseUrl}/${advisorId}/monthlytrend`);
  }
}
