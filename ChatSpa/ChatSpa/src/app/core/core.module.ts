import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";

import { HeaderComponent } from "./header/header.component";
import { SidebarComponent } from "./sidebar/sidebar.component";
import { FooterComponent } from "./footer/footer.component";
import { AuthService } from './services/auth.service';
import { AuthGuard } from './guards/auth.guard';
import { JwtModule } from '@auth0/angular-jwt';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './interceptors/auth.interceptor';

@NgModule({
  declarations: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    //JwtModule.forRoot({
    //  config: {        
    //    tokenGetter: tokenGetter,
    //        whitelistedDomains: ['localhost:5000','http://umrtest.com/beaconresource'],
    //        blacklistedRoutes: ['localhost:5000/api/user','http://umrtest.com/beaconresource/api/user']
    //  }
    //}),
      //JwtModule.forRoot({
      //    config: {
      //        tokenGetter: tokenGetter,
      //        whitelistedDomains: ['http://umrtest.com','http://umrtest.com/beaconresource','http://localhost'],
      //        blacklistedRoutes: ['http://umrtest.com/beaconresource/api/user']
      //    }
      //})
  ],
  providers: [
    AuthService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  exports: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent
  ]
})
export class CoreModule { }

export function tokenGetter() {
  return localStorage.getItem('token');
}
