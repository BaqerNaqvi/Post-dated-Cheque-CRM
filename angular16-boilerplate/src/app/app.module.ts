import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DuePaymentsComponent } from './pages/due-payments/due-payments.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { PaymentStatusComponent } from './shared/components/payment-status/payment-status.component';
import { MonthsComponent } from './shared/components/months/months.component';
import { PaymentMethodComponent } from './shared/components/payment-method/payment-method.component';
import { TopNavComponent } from './shared/components/top-nav/top-nav.component';
import { AgreementComponent } from './pages/agreement/agreement.component';
import { AddAgreementComponent } from './pages/add-agreement/add-agreement.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { InitializeSelect2Directive } from './shared/directives/initialize-select2.directive';
import { CompaniesListComponent } from './pages/company/companies-list/companies-list.component';
import { AddCompanyComponent } from './pages/company/add-company/add-company.component';
import { ImportPaymentsComponent } from './pages/import-payments/import-payments.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LoginComponent } from './pages/login/login.component';
@NgModule({
  declarations: [
    AppComponent,
    DuePaymentsComponent,
    PaymentStatusComponent,
    MonthsComponent,
    PaymentMethodComponent,
    TopNavComponent,
    AgreementComponent,
    AddAgreementComponent,
    InitializeSelect2Directive,
    CompaniesListComponent,
    AddCompanyComponent,
    ImportPaymentsComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
