import { Injectable } from '@angular/core';
import{ HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIUrl="http://localhost:49976/api";
  readonly PhotoUrl="http://localhost:49976/Photos/"
  constructor(private http:HttpClient) {}

  getDepList():Observable<any>{
    return this.http.get<any>(this.APIUrl+'/Department');
  }

  addDepartment(val:any){
    return this.http.post(this.APIUrl+'/Department',val);
  }

  updateDepartment(val:any){
    return this.http.put(this.APIUrl+'/Department',val);
  }

  deleteDepartment(val:any){
    return this.http.delete(this.APIUrl+'/Department/'+val);
  }




  getEmpList():Observable<any>{
    return this.http.get<any>(this.APIUrl+'/Emplyee');
  }

  addEmp(val:any){
    return this.http.post(this.APIUrl+'/Emplyee',val);
  }

  updatEmp(val:any){
    return this.http.put(this.APIUrl+'/Emplyee',val);
  }

  deleteEmp(val:any){
    return this.http.delete(this.APIUrl+'/Emplyee/'+val);
  }

  uploadPhoto(val:any){
    return  this.http.post(this.APIUrl+'/Emplyee/SaveFile',val,{});
  }
  getAllDep():Observable<any>{
    return this.http.get<any>(this.APIUrl+'/Emplyee/GetAllDepartment');
  }
}
