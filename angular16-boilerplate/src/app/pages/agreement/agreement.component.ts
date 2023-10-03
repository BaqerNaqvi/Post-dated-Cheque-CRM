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
  selector: 'app-agreement',
  templateUrl: './agreement.component.html',
  styleUrls: ['./agreement.component.css']
})
export class AgreementComponent implements AfterViewInit {
  selectedCompanyId: any;
  selectedMonth: string;
  selectedPaymentMethod: number;
  totalPrice: number;
  currentDate = new Date();
  selectedMonthParent: string = (this.currentDate.getMonth() + 1) + "-" + this.currentDate.getFullYear();

  agreementSearchFilter: any = {
    companyId: null,
    month: null,
    year: null
  }
  companies: Company[] = [];
  agreements: Agreement[] = [];

  constructor(
    public agreementService: AgreementService,
    public companyService: CompanyService) {

  }

  clearSearchFilters() {
    this.selectedCompanyId;
    this.selectedMonthParent = null;
    this.agreementSearchFilter.month = null;
    this.agreementSearchFilter.year = null;
    this.agreementSearchFilter.companyId = null;

    $('.select2bs4').val('').trigger('change');
  }

  onMonthSelected(month: any) {
    this.selectedMonth = month;
    this.agreementSearchFilter.month = month.split('-')[0];
    this.agreementSearchFilter.year = month.split('-')[1];
  }

  loading: boolean;
  paymentStatusOptions = Object.values(PaymentStatusEnum).map(value => ({ name: PaymentStatusEnum[value as number], value }));

  ngOnInit(): void {
    // this.getAllBanks();
    this.getAllCompannies();
    setTimeout(() => {
      this.jqueryScriptsBinding();
    }, 200);
  }

  ngAfterViewInit(): void {
    this.searchAgreements();
  }

  getCompanyName(id: number) {
    return this.companies.find(x => x.id == id)?.name
  }

  calculatePrices() {
    this.totalPrice = this.agreements.reduce((total, record) => {
      const descriptionValue = parseFloat(record.description);
      if (!isNaN(descriptionValue)) {
        return total + descriptionValue;
      }
      return total;
    }, 0);
  }

  searchAgreements() {
    // this.loading = true;
    setTimeout(() => {
      this.loading = true;
    }, 10);
    this.agreementService.Search(this.agreementSearchFilter).subscribe((result: any) => {
      this.agreements = result.data.sort((a: Agreement, b: Agreement) => a.startDate > b.startDate ? 1 : -1);
      this.calculatePrices();
      this.loading = false;
    });
  }

  getAgreementByCompanyId() {
    this.agreementService.GetByCompanyId(this.agreementSearchFilter.companyId).subscribe((result: any) => {
      this.agreements = result.data.sort((a: Agreement, b: Agreement) => a.startDate > b.startDate ? 1 : -1);
    });
  }

  getAllAgreements() {
    this.agreementService.GetAll().subscribe((result: any) => {
      this.agreements = result.data.sort((a: Agreement, b: Agreement) => a.startDate > b.startDate ? 1 : -1);
    });
  }

  getAllCompannies() {
    this.companyService.GetAll().subscribe((result: any) => {
      this.companies = result.data.sort((a: Company, b: Company) => a.name.toLowerCase() > b.name.toLowerCase() ? 1 : -1);
      this.companies.unshift({ id: null, name: 'All' });
    });
  }

  getPaymentMethodName(value: number): string {
    return PaymentMethodEnum[value];
  }
  getPaymentStatusName(value: number): string {
    return PaymentStatusEnum[value];
  }


  jqueryScriptsBinding() {
    $('.select2bs4').select2({
      theme: 'bootstrap4',
      placeholder: "Select an Option"
    });

    $('#companyDdl').on('select2:select', (e: any) => {
      var data = e.params.data;
      console.log(data);
      this.agreementSearchFilter.companyId = data.id == "null" ? null : data.id;
    });
  }
}
