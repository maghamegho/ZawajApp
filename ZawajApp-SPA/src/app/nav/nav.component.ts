import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private auth:AuthService) { }

  ngOnInit(): void {
  }

  login() {
    this.auth.login(this.model).subscribe(
      success=>{console.log('success');},
      error=>{console.log('error');}
    );
  }

  loggedin(){
    const token = localStorage.getItem('token');
    return !! token;
  }

  loggedout(){
    const token = localStorage.removeItem('token');
    console.log('تم الخروج');
  }
}
