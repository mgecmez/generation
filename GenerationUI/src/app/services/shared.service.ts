import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly APIUrl = "http://localhost:5005/api";

  constructor(private http: HttpClient) { }

  getPowerPlants(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/powerPlant');
  }

  getPowerPlant(id: any): Observable<any> {
    return this.http.get<any>(this.APIUrl + '/powerPlant/' + id);
  }

  addPowerPlant(val: any) {
    return this.http.post(this.APIUrl + '/powerPlant', val);
  }

  editPowerPlant(val: any) {
    return this.http.put(this.APIUrl + '/powerPlant', val);
  }

  deletePowerPlant(id: any) {
    return this.http.delete(this.APIUrl + '/powerPlant/' + id);
  }

  
}
