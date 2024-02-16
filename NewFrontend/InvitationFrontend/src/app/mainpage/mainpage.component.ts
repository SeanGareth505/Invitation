import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-mainpage',
  templateUrl: './mainpage.component.html',
  styleUrls: ['./mainpage.component.css']
})
export class MainpageComponent {
  invitationForm: FormGroup = new FormGroup({});

  constructor(private _fb: FormBuilder) {

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
    
  }
}
