import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Invoice } from '../models/invoice';

@Injectable({ providedIn: 'root' })
export class InvoicesService {

  private baseurl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getInvoices() {
    return this.http.get<Invoice[]>(`${this.baseurl}/api/invoices`);
  }

  runReminders() {
    return this.http.post(`${this.baseurl}/api/reminders/run`, {});
  }

  getSummary(invoices: Invoice[]) {

    return invoices.reduce((acc: any, inv) => {

      acc.total++;
      acc.byStatus[inv.status] = (acc.byStatus[inv.status] || 0) + 1;
      return acc;

    }, { total: 0, byStatus: {} as Record<string, number> });

  }

}
