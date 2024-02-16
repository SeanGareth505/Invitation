import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getInvitations(): Observable<any> {
    return this.http.get(`${this.baseUrl}/invitations`);
  }

  testInvitation(randomText: string): Observable<any> {
    const body = JSON.stringify({ randomText: randomText }); // Correctly format the body as a JSON string
    const headers = new HttpHeaders().set('Content-Type', 'application/json'); // Ensure headers are set to application/json

    return this.http.post(`${this.baseUrl}/Invitation/test`, body, { headers });
  }
}
