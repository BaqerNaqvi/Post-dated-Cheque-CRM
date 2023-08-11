import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { CompanyService } from 'src/app/services/company.service';
import { Company } from 'src/app/shared/models/company';
import { ActivatedRoute, Params, Router } from '@angular/router';
@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
  styleUrls: ['./add-company.component.css']
})
export class AddCompanyComponent implements OnInit {
  company: Company = new Company(0, '');
  validForm:boolean = true;
  constructor(private _location: Location, public companyService: CompanyService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      const id = params['id'];
      if (parseInt(id) > 0) {
        this.getCompanyById(id);
      }
    });
  }

  getCompanyById(id: number) {
    this.companyService.GetById(id).subscribe((result: any) => {
      this.company = result.data;
    });
  }
  backClicked() {
    this._location.back();
  }

  submitCompany() {
    if(this.company.name == ""){
      this.validForm = false
    }else{
      this.companyService.Create(this.company).subscribe(
        {
          next: (result: any) => {
            this.router.navigate(['companies']);
          },
          error: (e) => console.error(e),
          complete: () => console.info('complete')
        }
      );
    }
    
  }
}
