import { Component, OnInit } from '@angular/core';
import { CompanyService } from 'src/app/services/company.service';
import { Company } from 'src/app/shared/models/company';

@Component({
  selector: 'app-companies-list',
  templateUrl: './companies-list.component.html',
  styleUrls: ['./companies-list.component.css']
})
export class CompaniesListComponent implements OnInit {
  companies: Company[] = [];
  loading: boolean = false;
  constructor(public companyService: CompanyService) {
  }
  searchFilter: any = {
    name: null
  }
  ngOnInit(): void {
    this.searchCompanies();
  }

  clearSearchFilters() {
    this.searchFilter.name = null;
  }

  searchCompanies() {
    this.loading = true;
    this.companyService.Search(this.searchFilter).subscribe((result: any) => {
      this.companies = result.data.sort((a: Company, b: Company) => a.name > b.name ? 1 : -1);
      this.loading = false;
    });
  }
}
