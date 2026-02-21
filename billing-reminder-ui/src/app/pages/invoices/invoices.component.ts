import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoicesService } from '../../Services/invoices.service';
import { Invoice } from '../../models/invoice';

@Component({
  selector: 'app-invoices',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './invoices.component.html',
  styleUrls: ['./invoices.component.css']
})

export class InvoicesComponent implements OnInit {

  invoices: Invoice[] = [];
  summary: any = null;
  statusKeys: string[] = [];
  loading = false;
  error = '';

  constructor(private api: InvoicesService) { }

  ngOnInit(): void {
    this.reload();
  }

  reload() {
    this.loading = true;
    this.error = '';

    this.api.getInvoices().subscribe({
      next: (data) => {
        this.invoices = data;
        this.summary = this.api.getSummary(data);
        this.statusKeys = Object.keys(this.summary.byStatus);
        this.loading = false;
      },
      error: (e) => {
        this.error = 'No se pudo cargar la información.';
        this.loading = false;
      }
    });
  }

  run() {

    this.loading = true;
    this.error = '';

    this.api.runReminders().subscribe({

      next: () => this.reload(),

      error: () => {

        this.error = 'Falló ejecutar recordatorios.';
        this.loading = false;

      }

    });

  }

}
