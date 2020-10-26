import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //@Input() valuesFromHome:any;
  @Output() cancelRegister = new EventEmitter();
  model:any={};

  constructor(private auth:AuthService) { }

  ngOnInit(): void {
  }

  register(){
    this.auth.register(this.model).subscribe(
      ()=>{console.log('success');},
      error=>{console.log(error);}
    );
  }

  cancel(){
    console.log('cancel');
    this.cancelRegister.emit({"name":"cancel register", "status":false});
  }
}
