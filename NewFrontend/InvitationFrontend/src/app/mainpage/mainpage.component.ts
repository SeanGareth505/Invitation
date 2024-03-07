import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../services/api.service';
import { SubmitRSVPInputDTO } from '../Interfaces/InvitationInterfaces';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-mainpage',
  templateUrl: './mainpage.component.html',
  styleUrls: ['./mainpage.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class MainpageComponent {
  invitationForm: FormGroup = new FormGroup({});

  isClicked: boolean = false;
  isAccepted: boolean = false;
  isDeclined: boolean = false;

  handleAcceptClick() {
    this.isClicked = true;
    this.isAccepted = true;
    this.isDeclined = false;
  }

  handleDeclineClick() {
    this.isClicked = true;
    this.isAccepted = false;
    this.isDeclined = true;
  }

  constructor(private _fb: FormBuilder, private _apiService: ApiService, private snackBar: MatSnackBar, private _messageService: MessageService) {

  }

  ngOnInit(): void {
    this.resetForm();
  }

  resetForm() {
    this.invitationForm = this._fb.group({
      email: ['', [Validators.required, Validators.email]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      songRequest: [''],
      isAccepted: [false, Validators.required]
    });
  }

  onSubmit() {
    console.log('this._apiService.doesEmailExist(this.invitationForm.get(\'email\')?.value)', this._apiService.doesEmailExist(this.invitationForm.get('email')?.value))
    !this._apiService.doesEmailExist(this.invitationForm.get('email')?.value).subscribe({
      next: (doesExist) => {
        if (!doesExist) {
          if (this.invitationForm.valid) {
            let submitData: SubmitRSVPInputDTO = {
              email: this.invitationForm.get('email')?.value,
              firstName: this.invitationForm.get('firstName')?.value,
              lastName: this.invitationForm.get('lastName')?.value,
              songRequest: this.invitationForm.get('songRequest')?.value,
              isAccepted: this.isAccepted
            };

            this._apiService.submitRSVP(submitData).subscribe({
              next: (result) => {
                this._messageService.add({ severity: 'success', summary: 'RSVP Submitted', detail: 'RSVP Submitted Successfully' });
                this.isClicked = false;
                this.resetForm();
              },
              error: (error) => {
                console.error('Error submitting RSVP:', error);
                this._messageService.add({ severity: 'error', summary: 'Submission Error', detail: 'Could not submit RSVP. Please try again later or contact Michael.' });
              }
            });
          }
        } else {
          this._messageService.add({
            severity: 'error',
            summary: 'RSVP has already been submitted for this email',
            detail: 'RSVP has already been submitted for this email, if you want to resubmit please contact Michael.',
            life: 10000
          });

        }
      }
    })
  }
}
