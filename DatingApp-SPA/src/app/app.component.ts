import { Component, OnInit } from '@angular/core';
import { AuthService } from './_Services/auth.service';
import { JwtHelperService } from "@auth0/angular-jwt";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Chatty';
  helper = new JwtHelperService();
  constructor(private aauthservice:AuthService){}
  
  ngOnInit() {
    const token = localStorage.getItem('token');
    if(token){
      this.aauthservice.decodedToken = this.helper.decodeToken(token);
    }
  }
}
