import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DuePaymentsComponent } from './pages/due-payments/due-payments.component';
import { AgreementComponent } from './pages/agreement/agreement.component';
import { AddAgreementComponent } from './pages/add-agreement/add-agreement.component';
import { CompaniesListComponent } from './pages/company/companies-list/companies-list.component';
import { AddCompanyComponent } from './pages/company/add-company/add-company.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'due-payments',
    pathMatch: 'full'
  },
  { path: "due-payments", component: DuePaymentsComponent },
  { path: "agreements", component: AgreementComponent },
  { path: "agreements/add", component: AddAgreementComponent },
  { path: "agreements/:id", component: AddAgreementComponent },
  { path: "companies", component: CompaniesListComponent },
  { path: "companies/add", component: AddCompanyComponent },
  { path: "companies/:id", component: AddCompanyComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
