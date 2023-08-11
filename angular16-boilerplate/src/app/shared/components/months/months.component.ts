import { AfterViewInit, Component, EventEmitter, Input, Output } from '@angular/core';
declare var $: any;

interface Month {
  id: string;
  name: string;
}

@Component({
  selector: 'app-month-select',
  template: `<select [(ngModel)]="selectedMonth"  id="monthDdl" class="form-control select2bs4" style="width: 100%;">
  <option *ngFor="let month of months" [value]="month.id">{{ month.name }}</option>
</select>`,
})
export class MonthsComponent implements AfterViewInit {
  @Output() selectedMonthChange = new EventEmitter<string>();
  months: Month[] = [];
  currentDate = new Date();
  @Input() selectedMonth: string;// = (this.currentDate.getMonth() + 1) + "-" + this.currentDate.getFullYear(); // You can set the default selected month here if needed

  ngAfterViewInit(): void {
    this.populateMonths();
    // $('#monthDdl').select2({
    //   theme: 'bootstrap4',
    //   placeholder: "Select an Option"
    // });
    $('#monthDdl').on('select2:select', (e: any) => {
      const data = e.params.data;
      this.selectedMonth = data.id;
      this.selectedMonthChange.emit(this.selectedMonth);
    });
    this.selectedMonthChange.emit(this.selectedMonth);
  }

  populateMonths(): void {

    const currentMonth = this.currentDate.getMonth() - 6; // Get the current month (0-indexed)
    const currentYear = this.currentDate.getFullYear();

    // Add "ALL" option to the months array
    // this.months.push({ id: 0, name: 'All' });

    for (let i = 0; i < 24; i++) {
      const monthNumber = (currentMonth + i) % 12 || 12; // Loop back to December after January
      const year = currentYear + Math.floor((currentMonth + i - 1) / 12); // Increment year after December

      const month: Month = {
        id: monthNumber + '-' + year,
        name: this.getMonthWithYear(monthNumber, year),
      };

      this.months.push(month);
    }
  }

  getMonthWithYear(monthNumber: number, year: number): string {
    const monthNames: string[] = [
      'January',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'August',
      'September',
      'October',
      'November',
      'December',
    ];
    return `${monthNames[monthNumber - 1]}-${year}`;
  }
}
