import { Component, OnInit,Input } from '@angular/core';
import {SharedService} from 'src/app/shared.service';
@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.css']
})
export class AddEditEmpComponent implements OnInit {

  constructor(private service:SharedService) { }
  @Input() emp:any;
      Id:number;
      EmployeeName:string;
      Department:string;
      DateofJoning:string;
      Photo:string;
      PhotoFilePath:string;

      DepartmentList:any=[];

  ngOnInit(): void {
  this.loadDepartmentList();
  }

  loadDepartmentList(){
    this.service.getAllDep().subscribe(data=>{

      this.DepartmentList=data;
      this.Id=this.emp.Id;
      this.EmployeeName=this.emp.EmployeeName;
      this.Department=this.emp.Department;
      this.Department=this.emp.Department;
      this.Photo=this.emp.Photo;
      this.PhotoFilePath=this.service.PhotoUrl+this.Photo;
    });
  }
  addEmployee(){
    var val={Id: this.Id,EmployeeName:this.EmployeeName,Department:this.Department,DateofJoning:this.DateofJoning,Photo:this.Photo};
    this.service.addEmp(val).subscribe(res=>{
alert(res.toString())

    });

  }
  updateEmployee(){
    var val={Id: this.Id,EmployeeName:this.EmployeeName,Department:this.Department,DateofJoning:this.DateofJoning,Photo:this.Photo};
    this.service.updatEmp(val).subscribe(res=>{
alert(res.toString())

    });
}




uploadPhoto(event){
  var file=event.target.files[0];
  const formData:FormData=new FormData();
  formData.append('uploadedFile',file,file.name);

  this.service.uploadPhoto(formData).subscribe((data:any)=>{
    this.Photo=data.toString();
    this.PhotoFilePath=this.service.PhotoUrl+this.Photo;
  })
}





}