import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { UserList } from '../models/user.list';
import { UserDto } from '../models/user.dto';

@Injectable({
  providedIn: 'root'
})
export class UtilService {
  baseUrl: string = environment.apiUrl;
  pageNumber: number = 1;
  pageSize: number = 100;

  constructor(private http: HttpClient) { }

  getUsers(): Observable<UserList> {
    return this.http.get<UserList>(`${this.baseUrl}/users?PageNumber=${this.pageNumber}&PageSize=${this.pageSize}`).pipe(
      map((response: UserList) => {
        return response;
      })
    );
  }

  getUser(id: string): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.baseUrl}/users/${id}`).pipe(
      map((response: UserDto) => {
        return response;
      })
    );
  }
}
