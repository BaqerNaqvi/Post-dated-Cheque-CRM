import { Directive, ElementRef, AfterViewInit } from '@angular/core';
declare var $: any;

@Directive({
  selector: '[initializeSelect2]'
})
export class InitializeSelect2Directive implements AfterViewInit {
  constructor(private el: ElementRef) {}

  ngAfterViewInit(): void {
    $(this.el.nativeElement).select2({
      theme: 'bootstrap4',
      placeholder: 'Select an Option'
      // Add any additional options as needed
    });
  }
}