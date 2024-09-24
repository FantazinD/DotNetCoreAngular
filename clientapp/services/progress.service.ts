import { XhrFactory } from '@angular/common';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProgressService {
  uploadProgress: Subject<number> = new Subject();
  downloadProgress: Subject<number> = new Subject();

  uploadProgress$ = this.uploadProgress.asObservable();
  downloadProgress$ = this.downloadProgress.asObservable();

  updateUploadProgress = (progress: number) => {
    this.uploadProgress.next(progress);
  };

  updateDownloadProgress = (progress: number) => {
    this.downloadProgress.next(progress);
  };
}
