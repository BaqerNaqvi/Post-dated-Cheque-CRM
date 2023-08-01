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

@NgModule({
  declarations: [
    AppComponent,
    DuePaymentsComponent,
    PaymentStatusComponent,
    MonthsComponent,
    PaymentMethodComponent,
    TopNavComponent,
    AgreementComponent,
    AddAgreementComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
