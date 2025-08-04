import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../models/user.dto';
import { UtilService } from '../../services/util.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-users',
  standalone: false,
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  users: UserDto[] = [];
  totalRecords: number = 0;

  constructor(
    private utilService: UtilService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.utilService.getUsers().subscribe({
      next: (response) => {
        this.users = response.data;
        this.totalRecords = response.totalRecords;
      },
      error: (error) => {
        this.toastrService.error(error.error || 'Erro ao tentar buscar os usuários', 'Erro');
      }
    });
  }
}
