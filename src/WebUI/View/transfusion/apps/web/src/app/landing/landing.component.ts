import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'transfusion-root',
  templateUrl: './Landing.component.html',
  styleUrls: ['./Landing.component.scss'],
})
export class LandingComponent {
  title = 'web';

  @HostListener('window:popstate', ['$event'])
  onPopState() {
    location.reload()
  }
}
