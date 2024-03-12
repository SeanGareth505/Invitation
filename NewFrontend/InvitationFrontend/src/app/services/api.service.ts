import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { GetAllInvitationsOutputDTO, SubmitRSVPInputDTO } from '../Interfaces/InvitationInterfaces';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getInvitations(skip: number, take: number): Observable<GetAllInvitationsOutputDTO> {
    const params = new HttpParams()
      .set('skip', skip.toString())
      .set('take', take.toString());
  
    return this.http.get<GetAllInvitationsOutputDTO>(`${this.baseUrl}/invitation`, { params });
  }
  
  doesEmailExist(email: string): Observable<boolean> {
    console.log('email', email)
    return this.http.get<boolean>(`${this.baseUrl}/Invitation/CheckEmailExists/${email}`);
  }

  submitRSVP(randomText: SubmitRSVPInputDTO): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');

    return this.http.post(`${this.baseUrl}/Invitation/SubmitRSVP`, randomText, { headers });
  }
}
