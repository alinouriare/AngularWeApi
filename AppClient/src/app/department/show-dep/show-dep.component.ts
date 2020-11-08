import { Component, OnInit } from '@angular/core';
import {SharedService} from 'src/app/shared.service';
@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {

  constructor(private service:SharedService) { }
  DepartmentList :any=[];

  
  DepartmentIdFilter:string="";
  DepartmentNameFilter:string="";
  DepartmentListWithoutFilter:any=[];
 

  Modaltitle:string;
  ActivateAddEditcomp:boolean=false;
  dep:any;
  ngOnInit(): void {
    this.refreshDepList();
  }
  refreshDepList(){
    this.service.getDepList().subscribe(data=>{

      this.DepartmentList=data;
      this.DepartmentListWithoutFilter=data;

    });
   
}
  addClick(){

    this.dep={
      Id:0,
      DepartmentName:""
    }
    this.Modaltitle="Add Department";
    this.ActivateAddEditcomp=true;
  }
  closeClick(){
    this.ActivateAddEditcomp=false;
    this.refreshDepList();
  }
  editClick(item){
    this.dep=item;
    this.Modaltitle="Edit Department";
    this.ActivateAddEditcomp=true;

  }
  deleteClick(item){
    if (confirm('Are You Sure??')){
      this.service.deleteDepartment(item.Id).subscribe(res=>{

        alert(res.toString());
        this.refreshDepList();
      });
   
    }
  }
  FilterFn(){
    var DepartmentIdFilter = this.DepartmentIdFilter;
    var DepartmentNameFilter = this.DepartmentNameFilter;

    this.DepartmentList = this.DepartmentListWithoutFilter.filter(function (el){
        return el.Id.toString().toLowerCase().includes(
          DepartmentIdFilter.toString().trim().toLowerCase()
        )&&
        el.DepartmentName.toString().toLowerCase().includes(
          DepartmentNameFilter.toString().trim().toLowerCase()
        )
    });
  }

  sortResult(prop,asc){
    this.DepartmentList = this.DepartmentListWithoutFilter.sort(function(a,b){
      if(asc){
          return (a[prop]>b[prop])?1 : ((a[prop]<b[prop]) ?-1 :0);
      }else{
        return (b[prop]>a[prop])?1 : ((b[prop]<a[prop]) ?-1 :0);
      }
    })
  }
}