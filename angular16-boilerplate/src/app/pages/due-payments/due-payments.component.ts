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
  selector: 'app-due-payments',
  templateUrl: './due-payments.component.html',
  styleUrls: ['./due-payments.component.css']
})
export class DuePaymentsComponent implements AfterViewInit {
  selectedCompanyId: any;
  selectedMonth: string;
  selectedPaymentMethod: number;

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

  constructor(public paymentService: PaymentService, public agreementService: AgreementService, public bankService: BankService, public companyService: CompanyService) {

  }

  clearSearchFilters() {
    this.selectedCompanyId;
    this.selectedMonth;
    this.selectedPaymentMethod;


    this.paymentSearchFilter.agreementId = null;
    this.paymentSearchFilter.companyId = null;
    this.paymentSearchFilter.bankId = null;
    this.paymentSearchFilter.month = null;
    this.paymentSearchFilter.year = null;
    this.paymentSearchFilter.paymentMethodId = null;
    this.paymentSearchFilter.branch = null;

    $('.select2bs4').val('').trigger('change');
  }

  onPaymentMethodSelected(paymentMethodId: number) {
    this.selectedPaymentMethod = paymentMethodId;
    this.paymentSearchFilter.paymentMethodId = this.selectedPaymentMethod;
  }

  onMonthSelected(month: any) {
    this.selectedMonth = month;
    this.paymentSearchFilter.month = month.split('-')[0];
    this.paymentSearchFilter.year = month.split('-')[1];
  }

  paymentStatusOptions = Object.values(PaymentStatusEnum).map(value => ({ name: PaymentStatusEnum[value as number], value }));

  ngOnInit(): void {
    this.getAllBanks();
    this.getAllCompannies();
  }

  ngAfterViewInit(): void {
    this.jqueryScriptsBinding();
    this.searchPayment();
  }

  searchPayment() {
    this.paymentService.Search(this.paymentSearchFilter).subscribe((result: any) => {
      this.payments = result.data.sort((a: Payment, b: Payment) => a.paymentDueDate > b.paymentDueDate ? 1 : -1);
    });
  }

  getAgreementByCompanyId() {
    this.agreementService.GetByCompanyId(this.paymentSearchFilter.companyId).subscribe((result: any) => {
      this.agreements = result.data.sort((a: Agreement, b: Agreement) => a.startDate > b.startDate ? 1 : -1);
    });
  }



  getAllBanks() {
    this.bankService.GetAll().subscribe((result: any) => {
      this.banks = result.data.sort((a: Bank, b: Bank) => a.name.toLowerCase() > b.name.toLowerCase() ? 1 : -1);
    });
    // Add "All" option to the array
    // this.banks.push({ id: 0, name: 'All' });
  }

  getAllCompannies() {
    this.companyService.GetAll().subscribe((result: any) => {
      this.companies = result.data.sort((a: Company, b: Company) => a.name.toLowerCase() > b.name.toLowerCase() ? 1 : -1);
    });
    // Add "All" option to the array
    // this.companies.push({ id: 0, name: 'All' });
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
      this.paymentSearchFilter.companyId = data.id;
      this.getAgreementByCompanyId();
    });

    $('#agreementDdl').on('select2:select', (e: any) => {
      var data = e.params.data;
      console.log(data);
      this.paymentSearchFilter.agreementId = data.id;
    });

    $('#bankDdl').on('select2:select', (e: any) => {
      var data = e.params.data;
      this.paymentSearchFilter.bankId = data.id;
    });

    // ($("#example1") as any).DataTable({
    //   "responsive": true, "lengthChange": false, "autoWidth": false,
    //   "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    // }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');

    // ($('#example2') as any).DataTable({
    //   "paging": true,
    //   "lengthChange": false,
    //   "searching": false,
    //   "ordering": true,
    //   "info": true,
    //   "autoWidth": false,
    //   "responsive": true,
    // });
  }
}
