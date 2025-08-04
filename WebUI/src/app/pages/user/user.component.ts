import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserDto } from '../../models/user.dto';
import { UtilService } from '../../services/util.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user',
  standalone: false,
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {
  user: UserDto | null = null;
  id: string | null = null;
  
  constructor(
    private route: ActivatedRoute, 
    private utilService: UtilService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
      if (this.id)
        this.getUser(this.id);
    });
  }

  getUser(id: string) {
    this.utilService.getUser(id).subscribe({
      next: (response) => {
        this.user = response;
      },
      error: (error) => {        
        this.toastrService.error(error.error || 'Erro ao tentar buscar o usuário', 'Erro');
      }
    });
  }
}
