import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_Services/auth.service';
import { AlertifyService } from '../_Services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  userdto:any = {};
  constructor(public authService:AuthService ,
     private alertify:AlertifyService , 
     private router: Router) { }

  ngOnInit() {
  }

  login(){

    this.authService.login(this.userdto).subscribe(next=>{
      this.alertify.success("Logged in Successfully");

    },error=>{
      this.alertify.error(error);
    },()=>{
      this.router.navigate(['/members']);
    });
  }

  loggedIn(){
    
    return this.authService.loggedIn();
  }

  logOut(){
    localStorage.removeItem('token');
    this.alertify.message("Logged out ");
    this.router.navigate(['/home']);
  }

}
