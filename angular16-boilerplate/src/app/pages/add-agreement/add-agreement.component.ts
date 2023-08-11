import { AfterViewInit, Component, OnInit, ViewChildren, ElementRef, QueryList, ViewChild, NgZone, Injector, ApplicationRef, TemplateRef } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AgreementService } from 'src/app/services/agreement.service';
import { BankService } from 'src/app/services/bank.service';
import { CompanyService } from 'src/app/services/company.service';
import { PaymentService } from 'src/app/services/payment.service';
import { Constants } from 'src/app/shared/constants/app-constants';
import { InitializeSelect2Directive } from 'src/app/shared/directives/initialize-select2.directive';
import { PaymentMethodEnum } from 'src/app/shared/enums/payment_method.enum';
import { PaymentStatusEnum } from 'src/app/shared/enums/payment_status.enum';
import { Agreement } from 'src/app/shared/models/agreement';
import { Bank } from 'src/app/shared/models/bank';
import { Company } from 'src/app/shared/models/company';
import { Payment } from 'src/app/shared/models/payment';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
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
  paymentIdToDelete: number = 0;
  paymentIndexToDelete: number = 0;
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
  @ViewChild('dynamicDropdownContainer', { read: ElementRef }) dynamicDropdownContainer!: ElementRef;
  modalRef?: BsModalRef;

  invalidForm: boolean = false;
  constructor(public paymentService: PaymentService,
    public agreementService: AgreementService,
    public bankService: BankService,
    public companyService: CompanyService,
    private route: ActivatedRoute,
    private router: Router,
    private _location: Location,
    private injector: Injector,
    private appRef: ApplicationRef, private modalService: BsModalService) {

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
    setTimeout(() => {
      this.jqueryScriptsBinding();
    }, 500);
  }

  ngAfterViewInit() {
  }

  onPaymentMethodSelected(paymentMethodId: number, paymentItemIndex: number) {
    this.agreementPayments[paymentItemIndex].paymentMethod = paymentMethodId;
  }

  onPaymentMethodStatus(paymentStatusId: number, paymentItemIndex: number) {
    this.agreementPayments[paymentItemIndex].paymentStatus = paymentStatusId;
  }

  insertNewPayment() {
    this.agreementPayments.push(new Payment(0, 0, 0, moment(new Date()).format(Constants.DATE_FORMAT), 0, 0));
  }

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
    });
  }

  submitAgreement() {
    if (this.agreement.startDate == undefined || this.agreement.endDate == undefined || this.agreement.companyId == 0) {
      this.invalidForm = true;
    } else {
      this.agreement.startDate = moment(this.agreement.startDate, Constants.DATE_FORMAT).format('YYYY-MM-DD');
      this.agreement.endDate = moment(this.agreement.endDate, Constants.DATE_FORMAT).format('YYYY-MM-DD');
      this.agreementPayments = this.agreementPayments.map((x: Payment) => ({
        ...x,
        agreementId: this.agreement.id,
        paymentDueDate: moment(x.paymentDueDate, Constants.DATE_FORMAT).format('YYYY-MM-DD'),
        chequeDueDate: x.chequeDueDate != null ? moment(x.chequeDueDate, Constants.DATE_FORMAT).format('YYYY-MM-DD') : null,
        paymentClearanceDate: x.paymentClearanceDate != null ? moment(x.paymentClearanceDate, Constants.DATE_FORMAT).format('YYYY-MM-DD') : null
      }));

      this.agreementService.Create({
        ...this.agreement,
        "payments": this.agreementPayments
      }).subscribe(
        {
          next: (result: any) => {
            this.router.navigate(['agreements']);
          },
          error: (e) => console.error(e),
          complete: () => console.info('complete')
        }
      );
    }

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
  onCompanySelect(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.agreement.companyId = parseInt(target.value, 10);
  }

  backClicked() {
    this._location.back();
  }

  decline(): void {
    this.modalRef?.hide();
  }

  confirm(): void {
    this.modalRef?.hide();
    this.deletePayment(this.paymentIdToDelete);
  }

  removePaymentRow(indexToRemove: number, template: TemplateRef<any>) {
    var paymentToDelete = this.agreementPayments[indexToRemove];
    if (paymentToDelete.id > 0) {
      this.paymentIdToDelete = paymentToDelete.id;
      this.paymentIndexToDelete = indexToRemove;
      this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
    } else {
      this.agreementPayments.splice(indexToRemove, 1);
    }
  }

  deletePayment(paymentId: number) {
    this.paymentService.deletePayment(paymentId).subscribe(
      {
        next: (result: any) => {
          this.paymentIdToDelete = 0;
          this.agreementPayments.splice(this.paymentIndexToDelete, 1);
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      }
    );
  }

  jqueryScriptsBinding() {
    $('.select2bs4').select2({
      theme: 'bootstrap4',
      placeholder: "Select an Option"
    });

    $('#companyDdl').select2({
      theme: 'bootstrap4',
      placeholder: "Select an Option"
    }).on('select2:select', (e: any) => {
      const data = e.params.data;
      this.selectedCompanyId = data.id;
      this.agreement.companyId = data.id;
    });

    $(this.dynamicDropdownContainer.nativeElement).on('select2:select', (e: any) => {
      const data = e.params.data;
      const selectedIndex = e.target.getAttribute('data-index');
      if (selectedIndex !== null) {
        const index = parseInt(selectedIndex, 10);
        if (e.target.getAttribute('data-bank') == "sender")
          this.agreementPayments[index].senderBankId = data.id;
        else
          this.agreementPayments[index].receiverBankId = data.id;
      }
    });
  }
}
