import { Component, ViewChild, OnInit, ChangeDetectorRef } from '@angular/core';
import { ApiService } from '../services/api.service';
import { LazyLoadEvent } from 'primeng/api';
import { Table, TableLazyLoadEvent } from 'primeng/table';
import { GetAllInvitationsOutputDTO, Invitation } from '../Interfaces/InvitationInterfaces';

interface EmailOption {
  email: string;
}

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  invites: Invitation[] = [];
  loading: boolean = false;
  emailOptions: EmailOption[] = [];

  @ViewChild('dt2') dt2!: Table;

  // Pagination properties
  totalRecords: number = 0;
  first: number = 0;
  rows: number = 10;
  defaultRecordsCountPerPage = 10; // Example value, you can change it
  predefinedRecordsCountPerPage = [10, 25, 50];

  constructor(private _apiService: ApiService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    // this.loadInvitationsLazy({ first: this.first, rows: this.rows });
  }

  loadInvitationsLazy(event: any) {
    this.loading = true;
    this.first = event.first ?? 0;
    this.rows = event.rows ?? 10;
    
    console.log('event.filters', event.filters)
    this._apiService.getInvitations(   this.first, 
      this.rows, 
      event.filters && event.filters['firstName'] ? event.filters['firstName'].value : undefined, 
      event.filters && event.filters['lastName'] ? event.filters['lastName'].value : undefined, 
      event.filters && event.filters['isAccepted']?.value != undefined ? event.filters['isAccepted'].value : undefined).subscribe(
      (data) => {
        this.invites = data.invitations?? [];
        this.totalRecords = data.totalRecords;
        this.loading = false;
        this.cdr.detectChanges();
      },
      (error) => {
        console.error('Error loading invitations:', error);
        this.loading = false;
      }
    );
  }

  // Method to load more data as the user scrolls
  loadLazy(event: LazyLoadEvent) {
    this.loadInvitationsLazy(event);
  }

  // Method to filter globally
  filterGlobal(event: Event) {
    const element = event.target as HTMLInputElement;
    this.dt2.filterGlobal(element.value, 'contains');
  }
}
