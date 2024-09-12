import { ErrorHandler, Inject, Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  constructor(private toastrService: ToastrService) {}

  handleError(error: any): void {
    this.toastrService.error('An unexpected error happened.', 'Error', {
      timeOut: 5000,
      closeButton: true,
    });
  }
}
