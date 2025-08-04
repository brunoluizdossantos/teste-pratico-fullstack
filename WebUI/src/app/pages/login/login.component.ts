import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  userForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder, 
    private routerService: Router, 
    private authService: AuthService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.authService.logout();
    this.initializeForm();
  }

  initializeForm() {
    this.userForm = this.fb.group({
      email: ['', [Validators.required, Validators.email, Validators.minLength(3), Validators.maxLength(50)]],
      password: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]]
    });
  }

  submitForm() {
    if (this.userForm.valid) {
      this.authService.login(this.userForm.value).subscribe({
        next: () => {
          this.userForm.reset();
          this.routerService.navigate(['/admin/users']);
        },
        error: (error) => {
          this.toastrService.error(error.error || 'Falha no login. Tente novamente.', 'Erro');
        }
      });
    } else {
      this.toastrService.error('Formulário inválido. Verifique os campos.', 'Erro');
    }
  }
}
