import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
    {
        path: "",
        component: HomeComponent,
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {
                path: "dashboard",
                loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)                
            },
            {
                path: "user",
                loadChildren: () => import('./user/user.module').then(m => m.UserModule)
            } 
        ]
    },
    {
        path: 'login',
        loadChildren: () => import('./login/login.module').then(m => m.LoginModule)
    },
    {
        path: 'register',
        loadChildren: () => import('./registration/registration.module').then(m => m.RegistrationModule)
    },
    {
        path: '**',
        redirectTo: '',
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
