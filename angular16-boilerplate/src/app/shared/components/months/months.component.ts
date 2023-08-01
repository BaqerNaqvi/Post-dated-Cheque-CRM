import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
declare var $: any;

interface Month {
  id: string;
  name: string;
}

@Component({
  selector: 'app-month-select',
  templateUrl: './months.component.html'
})
export class MonthsComponent implements AfterViewInit {
  @Output() selectedMonthChange = new EventEmitter<number>();
  months: Month[] = [];

  selectedMonth: number = 0; // You can set the default selected month here if needed

  ngAfterViewInit(): void {
    this.populateMonths();

    $('#monthDdl').on('select2:select', (e: any) => {
      const data = e.params.data;
      console.log(data);
      this.selectedMonth = data.id;
      this.selectedMonthChange.emit(this.selectedMonth);
    });
  }

  populateMonths(): void {
    const currentDate = new Date();
    const currentMonth = currentDate.getMonth() + 1; // Get the current month (0-indexed)
    const currentYear = currentDate.getFullYear();

    // Add "ALL" option to the months array
    // this.months.push({ id: 0, name: 'All' });

    for (let i = 0; i < 12; i++) {
      const monthNumber = (currentMonth + i) % 12 || 12; // Loop back to December after January
      const year = currentYear + Math.floor((currentMonth + i - 1) / 12); // Increment year after December

      const month: Month = {
        id: monthNumber+'-'+year,
        name: this.getMonthWithYear(monthNumber, year)
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
      'December'
    ];
    return `${monthNames[monthNumber - 1]}-${year}`;
  }
}
