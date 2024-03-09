import { Component, ViewChild } from '@angular/core';
import { ApiService } from '../services/api.service';
import { GetAllInvitationsOutputDTO } from '../Interfaces/InvitationInterfaces';

interface EmailOption {
  email: string;
}


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  invites: GetAllInvitationsOutputDTO[] = [];
  loading: boolean = false;
  emailOptions: EmailOption[] = [];

  @ViewChild('dt2') dt2: any;

  constructor(private _apiService: ApiService) {
    this._apiService.getInvitations().subscribe((data) => {
      this.invites = data;
      this.emailOptions = data.map(x => ({ email: x.email }));
    })
  }

  ngOnInit(): void {
  }

  filterGlobal(event: Event) {
    const element = event.target as HTMLInputElement;
    this.dt2.filterGlobal(element.value, 'contains');
  }

}
