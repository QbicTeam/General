import { Component, OnInit, TestabilityRegistry } from '@angular/core';
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

  uploader:FileUploader;
  hasBaseDropZoneOver:boolean = false;
  hasAnotherDropZoneOver:boolean = false;
  baseUrl = environment.photosAPIUrl + "photos";

  constructor() { 
  }

  ngOnInit() {
    console.log(this.baseUrl);
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

    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };
  }

  onUpload() {
    console.log('Uploading photo...');
    this.uploader.uploadAll();
  }

}
