import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DuePaymentsComponent } from './pages/due-payments/due-payments.component';
import { AgreementComponent } from './pages/agreement/agreement.component';
import { AddAgreementComponent } from './pages/add-agreement/add-agreement.component';
import { CompaniesListComponent } from './pages/company/companies-list/companies-list.component';
import { AddCompanyComponent } from './pages/company/add-company/add-company.component';
import { ImportPaymentsComponent } from './pages/import-payments/import-payments.component';
import { authGuard } from './services/auth.guard';
import { LoginComponent } from './pages/login/login.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'due-payments',
    pathMatch: 'full'
  },
  { path: 'login', component: LoginComponent },
  { path: "due-payments", component: DuePaymentsComponent, canActivate: [authGuard] },
  { path: "agreements", component: AgreementComponent, canActivate: [authGuard] },
  { path: "agreements/add", component: AddAgreementComponent, canActivate: [authGuard] },
  { path: "agreements/:id", component: AddAgreementComponent, canActivate: [authGuard] },
  { path: "companies", component: CompaniesListComponent, canActivate: [authGuard] },
  { path: "companies/add", component: AddCompanyComponent, canActivate: [authGuard] },
  { path: "companies/:id", component: AddCompanyComponent, canActivate: [authGuard] },
  { path: "import", component: ImportPaymentsComponent, canActivate: [authGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
