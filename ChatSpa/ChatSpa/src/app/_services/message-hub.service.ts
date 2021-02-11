import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import * as signalR from '@aspnet/signalr';
import { Observable, Subject } from 'rxjs';
import { resourceServerRootUrl, resourceServerUrl } from '../shared/app-constant';
import { IMessageForAdd } from '../_models/Message/message-for-add';
import { MessageForList} from '../_models/Message/message-for-list';

@Injectable({
    providedIn: 'root'
})
export class MessageHubService {

    private sendMessageURI: string = `${resourceServerUrl}messages/send-message`;
    private getMessageByReceiverIdURI: string = `${resourceServerUrl}messages/get-message-by-receiver-id/`;
    public hubConnection: signalR.HubConnection | undefined;
    private sharedMessages = new Subject<MessageForList>();
    private receivedMessage: MessageForList = new MessageForList();

    constructor(private http: HttpClient) {
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
        this.hubConnection?.invoke('SendMessage', message )
            .catch(err => console.log(err));        
    }

    sendServerListener() {
        this.hubConnection?.on('BroadcastMessage', (message) => {            
            this.mapReceivedMessage(message);
        });
    }

    private mapReceivedMessage(message: MessageForList): void { 
        this.receivedMessage.text = message.text;        
        this.receivedMessage.receiverId = message.receiverId;
        this.sharedMessages.next(this.receivedMessage);        
    }

    public retrieveMappedMessage(): Observable<MessageForList> {
        return this.sharedMessages.asObservable();
    }
    
    sendMessage(model: IMessageForAdd) {
        return this.http.post(this.sendMessageURI, model, {
            headers: new HttpHeaders()
                .set('Content-Type', 'application/json'), observe: 'response', responseType: 'text'
        });         
    }

    getMessageByReceiverId(receiverId: string): Observable<HttpResponse<any>> {        
        return this.http.get(this.getMessageByReceiverIdURI + receiverId, { observe: 'response' });
    }
    
}
