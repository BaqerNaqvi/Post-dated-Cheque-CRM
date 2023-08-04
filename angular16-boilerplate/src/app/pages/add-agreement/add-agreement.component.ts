import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AgreementService } from 'src/app/services/agreement.service';
import { BankService } from 'src/app/services/bank.service';
import { CompanyService } from 'src/app/services/company.service';
import { PaymentService } from 'src/app/services/payment.service';
import { Constants } from 'src/app/shared/constants/app-constants';
import { PaymentMethodEnum } from 'src/app/shared/enums/payment_method.enum';
import { PaymentStatusEnum } from 'src/app/shared/enums/payment_status.enum';
import { Agreement } from 'src/app/shared/models/agreement';
import { Bank } from 'src/app/shared/models/bank';
import { Company } from 'src/app/shared/models/company';
import { Payment } from 'src/app/shared/models/payment';
declare var $: any;
declare var moment: any;

@Component({
  selector: 'app-add-agreement',
  templateUrl: './add-agreement.component.html',
  styleUrls: ['./add-agreement.component.css']
})
export class AddAgreementComponent implements AfterViewInit, OnInit {
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
  agreementPayments: Payment[] = [];
  agreement: Agreement = new Agreement(0, 0, moment(new Date()).format(Constants.DATE_FORMAT), moment(new Date()).format(Constants.DATE_FORMAT));

  constructor(public paymentService: PaymentService,
    public agreementService: AgreementService,
    public bankService: BankService,
    public companyService: CompanyService,
    private route: ActivatedRoute,
    private router: Router,
    private _location: Location) {

  }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const id = params['id'];
      if (parseInt(id) > 0) {
        this.getAgreementById(id);
      }
    });
    this.getAllBanks();
    this.getAllCompannies();
  }

  ngAfterViewInit(): void {
    this.jqueryScriptsBinding();
    // this.searchPayment();
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

  onPaymentMethodSelected(paymentMethodId: number, paymentItemIndex: number) {
    this.agreementPayments[paymentItemIndex].paymentMethod = paymentMethodId;
  }

  onMonthSelected(month: any) {
    this.selectedMonth = month;
    this.paymentSearchFilter.month = month.split('-')[0];
    this.paymentSearchFilter.year = month.split('-')[1];
  }

  paymentStatusOptions = Object.values(PaymentStatusEnum).map(value => ({ name: PaymentStatusEnum[value as number], value }));

  insertNewPayment() {
    this.agreementPayments.push(new Payment(0, 0, 0, moment(new Date()).format(Constants.DATE_FORMAT), 0, 0));

    // setTimeout(() => {
    //   this.jqueryScriptsBinding();
    // }, 100);
  }

  removePaymentRow(indexToRemove: number) {
    this.agreementPayments.splice(indexToRemove, 1);
  }

  // searchPayment() {
  //   this.paymentService.Search(this.paymentSearchFilter).subscribe((result: any) => {
  //     this.payments = result.data.sort((a: Payment, b: Payment) => a.paymentDueDate > b.paymentDueDate ? 1 : -1);
  //   });
  // }

  getAgreementById(id: number) {
    this.agreementService.GetById(id).subscribe((result: any) => {
      let agrmnt = result.data;
      agrmnt.startDate = moment(agrmnt.startDate).format(Constants.DATE_FORMAT);
      agrmnt.endDate = moment(agrmnt.endDate).format(Constants.DATE_FORMAT);
      this.agreement = agrmnt;
      this.agreementPayments = result.data.payments.map((x: Payment) => ({
        ...x,
        paymentDueDate: moment(x.paymentDueDate).format(Constants.DATE_FORMAT),
        chequeDueDate: x.chequeDueDate != null ? moment(x.chequeDueDate).format(Constants.DATE_FORMAT) : null,
        paymentClearanceDate: x.paymentClearanceDate != null ? moment(x.paymentClearanceDate).format(Constants.DATE_FORMAT) : null
      }));
      // setTimeout(() => {
      //   this.jqueryScriptsBinding();
      // }, 100);
    });


  }

  submitAgreement() {
    let payload = {
      "agreementDto": {
        ...this.agreement,
        "payments": this.agreementPayments
      }
    }
    console.log("submitted data:", payload)
    // this.agreementService.Create(payload).subscribe(
    //   {
    //     next: (result: any) => {
    //       this.router.navigate(['agreements']);
    //     },
    //     error: (e) => console.error(e),
    //     complete: () => console.info('complete')
    //   }
    // );
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

  backClicked() {
    this._location.back();
  }
  jqueryScriptsBinding() {
    $('.select2bs4').select2({
      theme: 'bootstrap4',
      placeholder: "Select an Option"
    });

    $('#companyDdl').on('select2:select', (e: any) => {
      var data = e.params.data;
      this.selectedCompanyId = data.id;
      this.agreement.companyId = data.id;
    });

    $('.date').datetimepicker({
      format: Constants.DATE_FORMAT
    });

    $("#startDate").on("change.datetimepicker", ({date, oldDate}) => {
      console.log("New date", date);
      console.log("Old date", oldDate);
      alert("Changed date")
})
  }
}
