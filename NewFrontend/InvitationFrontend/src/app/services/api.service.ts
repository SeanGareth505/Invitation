import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { SubmitRSVPInputDTO } from '../Interfaces/InvitationInterfaces';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getInvitations(): Observable<any> {
    return this.http.get(`${this.baseUrl}/invitations`);
  }

  submitRSVP(randomText: SubmitRSVPInputDTO): Observable<any> {
    // const body = JSON.stringify({ input: randomText });
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.http.post(`${this.baseUrl}/Invitation/SubmitRSVP`, randomText, { headers });
  }
}
