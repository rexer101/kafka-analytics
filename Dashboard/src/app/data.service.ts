import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule, HttpHeaders, HttpStatusCode } from '@angular/common/http';
import { Observable, catchError, exhaustMap, forkJoin, retry, throwError, timer } from 'rxjs';
import { CarData } from './app.component';
@Injectable({
  providedIn: 'root'
})
export class DataService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  constructor(private http: HttpClient) { }

  GetAlldata(): Observable<number> {
    return this.http
      .get<number>(
        "http://localhost:5102/RawData",
        this.httpOptions)
      .pipe(retry(1), catchError(this.errorHandl));
  }

  GetAlloverspeeding(): Observable<CarData[]> {
    return this.http
      .get<CarData[]>(
        "http://localhost:5102/Vehicle",
        this.httpOptions)
      .pipe(retry(1), catchError(this.errorHandl));
  }
    // Error handling
  errorHandl(error: any) {
      let errorMessage = '';
      if (error.error instanceof ErrorEvent) {
        // Get client-side error
        errorMessage = error.error.message;
      } else {
        // Get server-side error
        errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      }
      console.log(errorMessage);
      return throwError(() => {
        return errorMessage;
      });
    }
}
