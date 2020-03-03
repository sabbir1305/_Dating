import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_Services/auth.service';
import { AlertifyService } from '../_Services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  userdto:any = {};
  constructor(public authService:AuthService , private alertify:AlertifyService) { }

  ngOnInit() {
  }

  login(){

    this.authService.login(this.userdto).subscribe(next=>{
      this.alertify.success("Logged in Successfully");
    },error=>{
      this.alertify.error(error);
    });
  }

  loggedIn(){
    
    return this.authService.loggedIn();
  }

  logOut(){
    localStorage.removeItem('token');
    this.alertify.message("Logged out ");
  }

}
