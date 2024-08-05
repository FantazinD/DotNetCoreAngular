import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './components/home/home.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HomeComponent],
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
}
