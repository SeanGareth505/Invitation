import { Component } from '@angular/core';
import { ApiService } from './services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Invitation-app';
  value: string = '';

  constructor(private _apiService: ApiService) {

  }

  test() {
    this._apiService.testInvitation(this.value).subscribe({
      next: (response) => console.log('Test invitation sent successfully', response),
      error: (error) => console.error('Error sending test invitation', error)
    });
  }
}
