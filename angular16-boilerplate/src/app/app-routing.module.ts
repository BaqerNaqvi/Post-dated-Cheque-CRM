import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DuePaymentsComponent } from './pages/due-payments/due-payments.component';
import { AgreementComponent } from './pages/agreement/agreement.component';
import { AddAgreementComponent } from './pages/add-agreement/add-agreement.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'due-payments',
    pathMatch: 'full'
  },
  { path: "due-payments", component: DuePaymentsComponent },
  { path: "agreements", component: AgreementComponent },
  { path: "agreements/:id", component: AddAgreementComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
