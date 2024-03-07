import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { GetAllInvitationsOutputDTO, SubmitRSVPInputDTO } from '../Interfaces/InvitationInterfaces';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getInvitations(): Observable<GetAllInvitationsOutputDTO[]> {
    return this.http.get<GetAllInvitationsOutputDTO[]>(`${this.baseUrl}/invitation`);
  }

  submitRSVP(randomText: SubmitRSVPInputDTO): Observable<any> {
    // const body = JSON.stringify({ input: randomText });
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.http.post(`${this.baseUrl}/Invitation/SubmitRSVP`, randomText, { headers });
  }
}
