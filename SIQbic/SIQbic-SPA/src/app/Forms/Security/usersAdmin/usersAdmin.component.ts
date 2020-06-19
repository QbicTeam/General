import { Component, OnDestroy, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { AuthService } from 'src/app/securitas/_services/auth.service';
import { Subject } from 'rxjs';
import { AlertifyService } from 'src/app/_services/alertify.service';


@Component({
  selector: 'app-usersAdmin',
  templateUrl: './usersAdmin.component.html',
  styleUrls: ['./usersAdmin.component.css']
})
export class UsersAdminComponent implements OnDestroy, OnInit {

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  isLoadingInvitations = true;

  @Input() formData: any;
  invitations: any;
  currentInvite: any;
  userRoles: any;
  selectedRole: any;
  @ViewChild('closeNewInvitationModalBtn') closeBtn: ElementRef;
  @ViewChild('closeCancelInvitation') closeBtnCancel: ElementRef;
  @ViewChild('closeApproveInvitation') closeBtnApprove: ElementRef;
  
  constructor(private _authService: AuthService, private _alertify: AlertifyService) { }

  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };

    console.log('Form container loaded...', this.formData);
    this.loadInvitations();
    this.loadUserRoles();
  }
  
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  loadUserRoles() {
    this.userRoles = new Array();
    this.userRoles.push({"id": 0, "displayName": "Usuario"});
    this.userRoles.push({"id": 1, "displayName": "Operador"});
    this.userRoles.push({"id": 2, "displayName": "Administrador"});

  }

  loadInvitations() {
    this.isLoadingInvitations = true;

    this._authService.getInvitations().subscribe(data => {
      this.invitations = data;
      this.dtTrigger.next();
      this.isLoadingInvitations = false;
    });
  }

  onNewInvite() {
    this.currentInvite = {
      id:0,
      invitedEmail: "",
      code: "",
      dateCreated: ""
    }
  }

  onCreateInvite() {
    console.log(this.currentInvite);
    this.closeBtn.nativeElement.click();
    
    console.log(this.selectedRole);

    const invite = {
      roleId: this.selectedRole.id,
      invitedEmail: this.currentInvite.invitedEmail,
      sponsorEmail: this._authService.getDecodedToken().unique_name
    };

    this._authService.createInvitation(invite).subscribe(data => {
      this.invitations.push(data);
      this.sendInviteEmail(data["invitedEmail"], data["code"]);
    });
   

  }

  checkIfDueDate(dueDate) {
    let result = false;

    let invDate = new Date(dueDate);
    const currentDate = new Date();


    const formattedDueDate = invDate.getFullYear().toString() + ("0" + invDate.getMonth().toString()).slice(-2) + ("0" + invDate.getDate().toString()).slice(-2);
    const formattedDate = currentDate.getFullYear().toString() + ("0" + currentDate.getMonth().toString()).slice(-2) + ("0" + currentDate.getDate().toString()).slice(-2);
    
    if (formattedDueDate < formattedDate) result = true;

    return result;
  }

  onEdit(invitation: any) {
    this.currentInvite = invitation;
    this.selectedRole = this.userRoles.find(ur => ur.id == invitation.role.id);
    console.log(this.currentInvite);
  }

  onUpdate() {

    this.closeBtn.nativeElement.click();

    if (this.selectedRole.id !== this.currentInvite.role.id) {
      
      this._authService.updateInvitation(this.currentInvite.id, this.selectedRole.id).subscribe(() => { 
        this.invitations.find(i => i.id == this.currentInvite.id).role = this.selectedRole;
        this._alertify.success("Data Uodated Successfully");
      });
    }

  }

  onAprove(invitation: any) {
    this.currentInvite = invitation;
  }

  onCancel(invitation: any) {
    this.currentInvite = invitation;
  }

  onCancelSubmit() {

    this.closeBtnCancel.nativeElement.click();

    this._authService.cancellInvitation(this.currentInvite.id).subscribe(() => {
      this.currentInvite.status = "Cancelled";
      this._alertify.success("Invitation Cancelled");
    });

  }

  onAproveSubmit() {
    this.closeBtnApprove.nativeElement.click();

    this._authService.approveInvitation(this.currentInvite.id).subscribe(() => {
      this.currentInvite.status = "Accepted";
      this._alertify.success("Invitation Accepted");

      this.sendInviteEmail(this.currentInvite.invitedEmail, this.currentInvite.code)
    });    
  }

  sendInviteEmail(targetEmail: string, refCode: string) {

    
    const emailData = {
      "To": targetEmail,
      "Body": "",
      "Subject": "Invitation para registro a SiQbic",
      "IsHtml": true,
      "TemplateId": 3,
      "Values": [
        {"Variable":"codigo", "Value": refCode }
      ]
    };


    this._authService.sendInvitationEmail(emailData).subscribe(data => {
      this._alertify.success("User invited was notified!");
    }, err => {
      this._alertify.error(err);
    });

    // "To":"majahide.payan@hotmail.com",
    // "Body": "",
    // "Subject": "Test email with dinamyc html",
    // "IsHtml": true,
    // "TemplateId": 3,
    // "Values": [ { "Variable":"Nombre", "Value":"Gerardo" }, { "Variable":"company", "Value":"Lagza" }]

  }

}
