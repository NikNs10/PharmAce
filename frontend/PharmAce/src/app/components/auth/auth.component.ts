// import { Component } from '@angular/core';
// import { RouterOutlet } from '@angular/router';

// @Component({
//   selector: 'app-auth',
//   imports: [RouterOutlet],
//   templateUrl: './auth.component.html',
//   styleUrl: './auth.component.scss'
// })
// export class AuthComponent {

// }
import { CommonModule } from '@angular/common';
import { Component, AfterViewInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-auth',
  imports: [RouterOutlet , CommonModule , FormsModule],
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements AfterViewInit {
role: any;

  ngAfterViewInit(): void {
    const loginText = document.querySelector(".title-text .login") as HTMLElement;
    const loginBtn = document.querySelector("label.login") as HTMLElement;
    const signupBtn = document.querySelector("label.signup") as HTMLElement;
    const signupLink = document.querySelector("form .signup-link a") as HTMLElement;
    const sliderTab = document.querySelector(".slide-controls .slider-tab") as HTMLElement;
    const formInner = document.querySelector(".form-inner") as HTMLElement;
    const loginRadio = document.getElementById("login") as HTMLInputElement;
    const signupRadio = document.getElementById("signup") as HTMLInputElement;

    signupBtn.onclick = () => {
      formInner.style.transform = "translateX(-50%)";
      loginText.style.marginLeft = "-50%";
      sliderTab.style.left = "50%";
      loginRadio.checked = false;
      signupRadio.checked = true;
    };

    loginBtn.onclick = () => {
      formInner.style.transform = "translateX(0%)";
      loginText.style.marginLeft = "0%";
      sliderTab.style.left = "0%";
      loginRadio.checked = true;
      signupRadio.checked = false;
    };

    signupLink.onclick = (e) => {
      e.preventDefault();
      signupBtn.click();
    };
  }
}
