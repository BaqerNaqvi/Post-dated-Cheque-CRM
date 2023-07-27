import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DuePaymentsComponent } from './pages/due-payments/due-payments.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'due-payments',
    pathMatch: 'full'
  },
  { path: "due-payments", component: DuePaymentsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
