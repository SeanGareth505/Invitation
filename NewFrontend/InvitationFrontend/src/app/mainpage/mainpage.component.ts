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
    this.invitationForm = this._fb.group({
      email: ['', [Validators.required, Validators.email]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      songRequest: [''],
      isAccepted: [false, Validators.required]
    });
  }

  onSubmit() {
    if (this.invitationForm.valid) {
      let submitData: SubmitRSVPInputDTO = {
        email: this.invitationForm.get('email')?.value,
        firstName: this.invitationForm.get('firstName')?.value,
        lastName: this.invitationForm.get('lastName')?.value,
        songRequest: this.invitationForm.get('songRequest')?.value,
        isAccepted: this.isAccepted 
      };
      console.log('submitData', submitData)
  
      this._apiService.submitRSVP(submitData).subscribe(result => {
        this._messageService.add({severity:'success', summary:'RSVP Submitted', detail:'RSVP Submitted Successfully'});
      });
    }
  }
}
