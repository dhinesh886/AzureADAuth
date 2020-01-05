import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()

export class AuthService {
    private api_app_key: string = "f4b7e305-732c-45a7-9f03-83962e17bf3e";
    private client_app_key: string = "acb09f7d-8a3d-4371-bb41-ea626e919339";
    private client_secret: string = "6e_G9K?3X0J[=n:67Iqv.rmqvuCd9oW/";
    private directoryName: string = "dhinesh8586gmail.onmicrosoft.com";


    constructor(private _http: Http) {

    }

    Authenticate(userName: string, password: string) {


        let payload = {
            client_app_key: this.client_app_key,
            username: userName,
            password: password,
            client_secret: this.client_secret
        };

        let url = "https://login.windows.net/" + this.directoryName + "/oauth2/token";

        return this._http.post(url, payload)
            .map(response => response.json());

        //let payload  = {
        //    client_app_key: this.client_app_key,
        //    username: userName,
        //    password: password,
        //    client_secret: this.client_secret
        //};

        //let url = "https://login.windows.net/" + this.directoryName +"/oauth2/token";

        //return this._http.post("http://localhost:56164/api/auth/Login", payload)
        //    .map(response => response.json());
    }

    GetBusinessData(id: string) {
        let httpOptions = {
            headers: new Headers({
                grand_type: 'password',
                resource: this.api_app_key,
                client_id: this.client_app_key,
                //username: fullUserName,
                //password: password,
                scope: 'openid',
                client_secret: this.client_secret
            })
        };
        return this._http.get("https://jsonplaceholder.typicode.com/posts")
            .map(response => response.json());
    }
}