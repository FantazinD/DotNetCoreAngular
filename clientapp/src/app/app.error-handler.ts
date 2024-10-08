import { ErrorHandler, Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  constructor(private toastrService: ToastrService) {}

  handleError(error: any): void {
    this.toastrService.error('An unexpected error happened.', 'Error', {
      timeOut: 3000,
      closeButton: true,
    });
  }
}
