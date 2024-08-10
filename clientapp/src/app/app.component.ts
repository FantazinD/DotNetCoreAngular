import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { FormsModule } from '@angular/forms';
import { NgFor, NgIf, NgTemplateOutlet } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    HomeComponent,
    FormsModule,
    NgIf,
    NgTemplateOutlet,
    NgFor,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title: string = 'Vega';
  imgUrl: string =
    'https://yt3.googleusercontent.com/y4J_Fs5ksRlxx6_LzT1VKxVqH_T8Vyn_RN_YYgLJhuMzBS5qxTgm7NlEcMkQd3hgCpfWtYcEUg=s900-c-k-c0x00ffffff-no-rj';
  isDisabled: boolean = true;
  isActive: boolean = true;
  fruitName: string = 'Apple';
  isLoggedIn: boolean = true;
  userName: string = 'John Doe';
  buttonClick = () => {
    console.log('hehe');
  };
  keyEnter = (event: any) => {
    if (event.keyCode == 13) {
      console.log('Enter Key Pressed.');
    }
  };
  keyupFiltering = (user: HTMLInputElement) => {
    console.log(user.id);
  };
  updateUsername = (username: HTMLInputElement) => {
    this.userName = username.value;
    console.log(this.userName);
  };
  loginAttempts: number = 0;
  countLoginAttempts = () => {
    this.loginAttempts++;
  };
  userRole: string = 'Admin';
  users: Array<string> = ['John', 'Sam', 'Smith', 'Heel'];
  usersObj: Array<any> = [
    { Name: 'John', Id: 1 },
    { Name: 'Sam', Id: 2 },
    { Name: 'Smith', Id: 3 },
    { Name: 'Heel', Id: 4 },
  ];
}
