import { AfterViewInit, Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { PaymentMethodEnum } from '../../enums/payment_method.enum';
declare var $: any;

interface PaymentMethod {
  id: number;
  name: string;
}

@Component({
  selector: 'app-payment-method-select',
  template: `
    <select  #paymentMethodDdl [(ngModel)]="selectedPaymentMethod" (change)="onPaymentMethodChange()" class="form-control select2bs4" style="width: 100%;">
      <option *ngFor="let method of paymentMethods" [value]="method.id">{{ method.name }}</option>
    </select>
  `,
})
export class PaymentMethodComponent implements AfterViewInit {
  @ViewChild('paymentMethodDdl') paymentMethodDdl: ElementRef;
  @Output() selectedPaymentMethodChange = new EventEmitter<number>();

  paymentMethods: PaymentMethod[] = [
    { id: null, name: 'All' },
    { id: PaymentMethodEnum.Cheque, name: 'Cheque' },
    { id: PaymentMethodEnum.Cash, name: 'Cash' },
    // { id: PaymentMethodEnum.Advanced, name: 'Advanced' },
    { id: PaymentMethodEnum.Online, name: 'Online' },
  ];

  @Input() selectedPaymentMethod: number;

  onPaymentMethodChange() {
    this.selectedPaymentMethodChange.emit(this.selectedPaymentMethod);
  }

  ngAfterViewInit(): void {
    this.paymentMethods = this.paymentMethods.sort((a: PaymentMethod, b: PaymentMethod) => a.name > b.name ? 1 : -1)
    $(this.paymentMethodDdl.nativeElement).on('select2:select', (e: any) => {
      var data = e.params.data;
      this.selectedPaymentMethodChange.emit(data.id);
    });
  }
}
