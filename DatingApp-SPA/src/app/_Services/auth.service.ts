import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = "http://localhost:5000/api/auth/";
constructor(private httpClient:HttpClient) { }


login(userDto:any){
  return this.httpClient.post(this.baseUrl+'login',userDto)
  .pipe(
    map((response: any)=>{

      const user = response;
      if(user){
        localStorage.setItem('token',user.token); 
      }
    }
    
    )
  )
}

register(model: any){
  return this.httpClient.post(this.baseUrl+ 'Register',model);
}


}

