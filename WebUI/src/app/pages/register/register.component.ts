import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  userForm: FormGroup = new FormGroup({});
  errorResponse: string | undefined = undefined;
  successResponse: boolean | undefined = undefined;
  userEmail: string | undefined = undefined;
  userPassword: string | undefined = undefined;

  constructor(
    private fb: FormBuilder, 
    private authService: AuthService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.userForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.email, Validators.minLength(3), Validators.maxLength(50)]]
    });
  }

  submitForm() {
    if (this.userForm.valid) {
      this.authService.register(this.userForm.value).subscribe({
        next: (response) => {
          this.toastrService.success('Cadastro realizado com sucesso!', 'Sucesso');
          this.setUserData(response);
        },
        error: (error) => {
          this.clearSuccess();
          this.toastrService.error(error.error || 'Falha no login. Tente novamente.', 'Erro');
        }
      });
    } else {
      this.toastrService.error('Formulário inválido. Verifique os campos.', 'Erro');
    }
  }

  setUserData(response: any) {
    this.userEmail = response.email;
    this.userPassword = response.password;
    this.successResponse = true;
    this.clearError();
  }

  clearError() {
    this.errorResponse = undefined;
  }

  clearSuccess() {
    this.successResponse = undefined;
    this.clearUserData();
  }

  clearUserData() {
    this.userEmail = undefined;
    this.userPassword = undefined;
  }

  clearForm() {
    this.userForm.reset();
    this.clearError();
  }
}