import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';

// const URL = '/api/';
const URL = 'https://evening-anchorage-3159.herokuapp.com/api/';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Output() onSuccessPhotoUploaded = new EventEmitter();

  uploader:FileUploader;
  hasBaseDropZoneOver:boolean = false;
  hasAnotherDropZoneOver:boolean = false;
  baseUrl = environment.photosAPIUrl + "photos/" + environment.profilesPhotosProjectName + "/" + environment.profilesPhotosFolderName + "/photos/";

  
  constructor() { 
  }

  ngOnInit() {
    this.initializeUploader();
  }

  public fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
  }
  
  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl, 
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => { 
      file.withCredentials = false; 
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => this.onSuccessPhoto(item, response, status, headers);

  }

  onSuccessPhoto(item: any, response: any, status: any, headers: any) {
    const data = JSON.parse(response);

    this.onSuccessPhotoUploaded.emit(environment.profilesPhotosRepoUrl +  environment.profilesPhotosProjectName 
        + "/" + environment.profilesPhotosFolderName + "/" + data.url);
  }

  onUpload() {
    if (this.uploader.queue.length > 0) {
      this.uploader.uploadAll();
    } else {
      this.onSuccessPhotoUploaded.emit("");
    }
  }

}
