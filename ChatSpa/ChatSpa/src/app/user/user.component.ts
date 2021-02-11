import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { Message, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { resourceServerRootUrl } from '../shared/app-constant';
import { IMessageForAdd } from '../_models/Message/message-for-add';
import { MessageForList } from '../_models/Message/message-for-list';
import { MessageHubService } from '../_services/message-hub.service';
import { UserService } from '../_services/user.service';
import * as signalR from '@aspnet/signalr';

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit, OnDestroy {

    @ViewChild('userTable', { static: false })
    userTable!: Table;
    users: Array<any> = [];
    selectedUser!: MessageForList;
    messages: Array<MessageForList> = [];
    messageFormGroup!: FormGroup;
    selectedReceiverId!: string;
    hideChat: boolean = true;
    hubConnection: signalR.HubConnection | undefined;
    @ViewChild('messageDiv', { static: false })
    messageDiv!: ElementRef;
    messageScrollTop: number = 999999999;
    receiverName!: string;

    constructor(private fb: FormBuilder, private spinnerService: NgxSpinnerService, private messageService: MessageService, private userService: UserService, private messageHubService: MessageHubService) { }    

    ngOnInit() {
        this.createMessageForm();
        this.getAllUser();
        this.startConnection();               
    }    

    public getUserId() {
        return localStorage.getItem("id");
    }

    public userName() {
        return localStorage.getItem('username');
    }

    startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${resourceServerRootUrl}messagehub`, {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            }).build();

        this.hubConnection.start()
            .then(() => {
                console.log('Hub Connection Started!');
            })
            .catch(err => console.log('Error while starting connection: ' + err));
    }

    broadcastMessage(message: IMessageForAdd) {
        this.hubConnection?.invoke('SendMessage', message)
            .catch(err => console.log(err));
    }

    sendServerListener() {
        this.hubConnection?.on('BroadcastMessage', (message) => {
            console.log(message)
            this.messages.push(message);
        });
    }

    createMessageForm() {
        this.messageFormGroup = this.fb.group({
            text: ['', [this.noWhitespaceValidator, Validators.maxLength(1024)]]
        });
    }

    noWhitespaceValidator(control: AbstractControl) {
        if (control && control.value && !control.value.replace(/\s/g, '').length) {
            control.setValue('');
        }
        return null;
    }

    getAllUser() {
        this.spinnerService.show();
        this.userService.getAllUserExceptById()
            .subscribe(data => {
                if (data.status === 200) {
                    this.users = data.body;
                    console.log(data.body);
                }
                else {
                    this.users = [];
                }
            },
                err => {
                    this.spinnerService.hide();
                    this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Failed to get users', detail: '' });
                },
                () => {
                    this.spinnerService.hide();
                });
    }

    getMessageByReceiverId(receiverId: string) {
        this.spinnerService.show();
        this.messageHubService.getMessageByReceiverId(receiverId)
            .subscribe(data => {
                if (data.status === 200) {
                    this.messages = data.body;                    
                }
                else {
                    this.messages = [];
                }
            },
                err => {
                    this.spinnerService.hide();
                    this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Failed to get messages', detail: '' });
                },
                () => {
                    this.spinnerService.hide();
                });
    }

    getUserById(userId: string) {
        this.spinnerService.show();
        this.userService.getUserById(userId)
            .subscribe(response => {                
                if (response.status === 200) {
                    this.receiverName = (response.body as any).firstName + ' ' + (response.body as any).lastName;                    
                }                
            },
                err => {                    
                    this.spinnerService.hide();
                    this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Failed to get receiver user', detail: '' });
                },
                () => {
                    this.spinnerService.hide();
                });
    }        

    onRowSelect(event: any) {
        this.hideChat = false;
        this.messageFormGroup.controls.text.setValue('');
        this.selectedReceiverId = this.selectedUser.id;
        this.getUserById(this.selectedReceiverId);
        this.getMessageByReceiverId(this.selectedReceiverId);

        setTimeout(() => {
            this.sendServerListener();
            let scrollHeight = this.messageDiv.nativeElement.scrollHeight;
            this.messageScrollTop = scrollHeight+100;
        }, 200);   
    }

    paginate(event: any) {
        this.spinnerService.show();
        let pageIndex = event.first / event.rows + 1 // Index of the new page if event.page not defined.        
        let paging = {
            first: ((pageIndex - 1) * this.userTable.rows),
            rows: this.userTable.rows
        };
        this.spinnerService.hide();
    }    

    onSendMessage() {
        this.spinnerService.show();
        const message: IMessageForAdd = {
            text: this.messageFormGroup.controls.text.value.trim(),
            receiverId: this.selectedReceiverId
        };

        this.messageHubService.sendMessage(message)
            .subscribe(response => {
                if (response.status === 200) {
                    this.messageService.add({ key: 'toastKey1', severity: 'success', summary: 'Message has been sent successfully', detail: '' });                    
                }
            },
                (error: HttpErrorResponse) => {
                    this.spinnerService.hide();
                },
                () => {
                    this.spinnerService.hide();
                });

        setTimeout(() => {
            let scrollHeight = this.messageDiv.nativeElement.scrollHeight;
            this.messageScrollTop = scrollHeight;
        }, 100);

        this.messageFormGroup.controls.text.setValue('');        
    }

    ngOnDestroy() {
        this.messageHubService.hubConnection?.off('sendMessageResponse');
    }
}
