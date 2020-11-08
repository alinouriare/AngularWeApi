import { Component, OnInit } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.css']
})
export class ShowEmpComponent implements OnInit {

  constructor(private service:SharedService) { }
  emp:any;
  ActivateAddEditemp:boolean=false;
  Modaltitle:string="";
  EmployeeList:any=[];

  ngOnInit(): void {
    this.refreshEmpList();
  }

  addClick(){
    this.Modaltitle="Add Employee";
    this.ActivateAddEditemp=true;
    this.emp={
      Id:0,
      EmployeeName:"",
      Department:"",
      DateofJoning:"",
      Photo:"non.jpg"

    };
  }
  editClick(item){
    this.Modaltitle="Edit Employee";
    this.ActivateAddEditemp=true;
    this.emp=item;
  }


  refreshEmpList(){
    this.service.getEmpList().subscribe(data=>{

      this.EmployeeList=data;
    });
  }

  closeClick(){
    this.ActivateAddEditemp=false;
    this.refreshEmpList();
  }

  deleteClick(item){
    if (confirm('Are You Sure??')){
      this.service.deleteEmp(item.Id).subscribe(res=>{

        alert(res.toString());
        this.refreshEmpList();
      });
   
    }
}
}
