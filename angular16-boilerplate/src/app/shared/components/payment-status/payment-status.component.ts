import { AfterViewInit, Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { PaymentStatusEnum } from '../../enums/payment_status.enum';
declare var $: any;

interface PaymentStatus {
  id: number;
  name: string;
}

@Component({
  selector: 'app-payment-status-select',
  template: `
    <select  #paymentStatusDdl [(ngModel)]="selectedPaymentStatus" (change)="onPaymentStatusChange()" class="form-control select2bs4" style="width: 100%;">
      <option *ngFor="let method of paymentStatuses" [value]="method.id">{{ method.name }}</option>
    </select>
  `,
})
export class PaymentStatusComponent implements AfterViewInit {
  @ViewChild('PaymentStatusDdl') PaymentStatusDdl: ElementRef;
  @Output() selectedPaymentStatusChange = new EventEmitter<number>();

  paymentStatuses: PaymentStatus[] = [
    { id: PaymentStatusEnum.Pending, name: 'Pending' },
    { id: PaymentStatusEnum.Paid, name: 'Paid' },
    { id: PaymentStatusEnum.Late, name: 'Late' },
    { id: PaymentStatusEnum.Bounced, name: 'Bounced' },
    { id: PaymentStatusEnum.Cancelled, name: 'Cancelled' },
  ];

  @Input() selectedPaymentStatus: number;

  onPaymentStatusChange() {
    this.selectedPaymentStatusChange.emit(this.selectedPaymentStatus);
  }

  ngAfterViewInit(): void {
    this.paymentStatuses = this.paymentStatuses.sort((a: PaymentStatus, b: PaymentStatus) => a.name > b.name ? 1 : -1)
    $(this.PaymentStatusDdl.nativeElement).on('select2:select', (e: any) => {
      var data = e.params.data;
      this.selectedPaymentStatusChange.emit(data.id);
    });
  }
}
