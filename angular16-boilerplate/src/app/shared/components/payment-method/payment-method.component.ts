import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
declare var $: any;
enum PaymentMethodEnum {
  Cheque,
  Cash,
  Advanced,
  Online,
}

interface PaymentMethod   {
  id: number;
  name: string;
}

@Component({
  selector: 'app-payment-method-select',
  template: `
    <select [(ngModel)]="selectedPaymentMethod" (change)="onPaymentMethodChange()" id="paymentStatusDdl" class="form-control select2bs4" style="width: 100%;">
      <option *ngFor="let method of paymentMethods" [value]="method.id">{{ method.name }}</option>
    </select>
  `,
})
export class PaymentMethodComponent implements AfterViewInit {
  @Output() selectedPaymentMethodChange = new EventEmitter<number>();

  paymentMethods: PaymentMethod[] = [
    { id: PaymentMethodEnum.Cheque, name: 'Cheque' },
    { id: PaymentMethodEnum.Cash, name: 'Cash' },
    { id: PaymentMethodEnum.Advanced, name: 'Advanced' },
    { id: PaymentMethodEnum.Online, name: 'Online' },
  ];

  selectedPaymentMethod: number;

  onPaymentMethodChange() {
    this.selectedPaymentMethodChange.emit(this.selectedPaymentMethod);
  }

  ngAfterViewInit(): void {
    $('#paymentStatusDdl').on('select2:select', (e: any) => {
      var data = e.params.data;
      this.selectedPaymentMethodChange.emit(data.id);
    });
  }
}