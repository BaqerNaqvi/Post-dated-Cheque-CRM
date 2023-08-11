import { AfterViewInit, Component } from '@angular/core';
import { AgreementService } from 'src/app/services/agreement.service';
import { BankService } from 'src/app/services/bank.service';
import { CompanyService } from 'src/app/services/company.service';
import { PaymentService } from 'src/app/services/payment.service';
import { PaymentMethodEnum } from 'src/app/shared/enums/payment_method.enum';
import { PaymentStatusEnum } from 'src/app/shared/enums/payment_status.enum';
import { Agreement } from 'src/app/shared/models/agreement';
import { Bank } from 'src/app/shared/models/bank';
import { Company } from 'src/app/shared/models/company';
import { Payment } from 'src/app/shared/models/payment';

declare var $: any;

@Component({
  selector: 'app-import-payments',
  templateUrl: './import-payments.component.html',
  styleUrls: ['./import-payments.component.css']
})
export class ImportPaymentsComponent {
  selectedCompanyId: any;
  selectedMonth: string;
  selectedPaymentMethod: number;
  xlsFile!: File;
  paymentSearchFilter: any = {
    agreementId: null,
    companyId: null,
    bankId: null,
    month: null,
    year: null,
    paymentMethodId: null,
    branch: null
  }
  companies: Company[] = [];
  banks: Bank[] = [];
  payments: Payment[] = [];
  agreements: Agreement[] = [];
  loading: boolean = false;
  selectedFileName: string = 'Choose a file';

  constructor(public paymentService: PaymentService, public agreementService: AgreementService, public bankService: BankService, public companyService: CompanyService) {

  }

  paymentStatusOptions = Object.values(PaymentStatusEnum).map(value => ({ name: PaymentStatusEnum[value as number], value }));

  onFileChange(event: any) {
    const files = event.target.files;
    if (files && files.length > 0) {
      this.xlsFile = files[0];
    }

    const inputFile = event.target as HTMLInputElement;
    if (inputFile.files && inputFile.files.length > 0) {
      this.selectedFileName = inputFile.files[0].name;
    } else {
      this.selectedFileName = 'Choose file';
    }
  }

  uploadFile() {
    if (this.xlsFile != null) {
      this.loading = true;
      this.paymentService.Import(this.xlsFile).subscribe(
        {
          next: (result: any) => {
            this.payments = result.data;
            this.loading = false;
          },
          error: (e) => { console.error(e); this.loading = false; },
          complete: () => { console.info('complete'); this.loading = false; }
        }
      );
    }
  }

  getPaymentMethodName(value: number): string {
    return PaymentMethodEnum[value];
  }
  getPaymentStatusName(value: number): string {
    return PaymentStatusEnum[value];
  }
}
