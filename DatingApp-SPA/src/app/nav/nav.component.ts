import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_Services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  userdto:any = {};
  constructor(private authService:AuthService) { }

  ngOnInit() {
  }

  login(){

    this.authService.login(this.userdto).subscribe(next=>{
      console.log("Logged in Successfully");
    },error=>{
      console.log(error);
    });
  }

  loggedIn(){
    const token = localStorage.getItem('token');

    return !!token;
  }

  logOut(){
    localStorage.removeItem('token');
    console.log("Logged out success");
  }

}
