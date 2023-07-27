import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
declare var $: any;

interface Month {
  id: number;
  name: string;
}

@Component({
  selector: 'app-month-select',
  templateUrl: './months.component.html'
})
export class MonthsComponent implements AfterViewInit {
  @Output() selectedMonthChange = new EventEmitter<number>();
  months: Month[] = [
    { id: 1, name: 'January' },
    { id: 2, name: 'February' },
    { id: 3, name: 'March' },
    { id: 4, name: 'April' },
    { id: 5, name: 'May' },
    { id: 6, name: 'June' },
    { id: 7, name: 'July' },
    { id: 8, name: 'August' },
    { id: 9, name: 'September' },
    { id: 10, name: 'October' },
    { id: 11, name: 'November' },
    { id: 12, name: 'December' },
  ];

  selectedMonth: number = 0; // You can set the default selected month here if needed

  ngAfterViewInit(): void {
    $('#monthDdl').on('select2:select', (e: any) => {
      var data = e.params.data;
      console.log(data);
      this.selectedMonth = data.id;
      this.selectedMonthChange.emit(this.selectedMonth);
    });
  }
  
}
