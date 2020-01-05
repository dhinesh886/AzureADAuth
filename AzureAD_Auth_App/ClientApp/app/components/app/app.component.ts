import { Component } from '@angular/core';
import { AuthService } from '../../Services/AuthService';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    userName: string = "";
    password: string = "";

    constructor(private _service: AuthService) {

    }
    OnLogin() {
        this._service.Authenticate(this.userName, this.password)
            .subscribe(res => {
                console.log(res);
            });
    }
}
