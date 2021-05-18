import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateCastComponent } from './auth/create-cast/create-cast.component';
import { CreateMovieComponent } from './auth/create-movie/create-movie.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { HomeComponent } from './home/home.component';
import { MovieCardListComponent } from './movies/movie-card-list/movie-card-list.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';

// specify all the routes belonging to Angular application
const routes: Routes =
  [
    { path: "", component: HomeComponent },
    { path: "login", component: LoginComponent },
    { path: "register", component: RegisterComponent },
    { path: "movie/:id", component: MovieDetailsComponent },
    { path: "genres/:id", component:  MovieCardListComponent},
    { path: "admin/createmovie", component: CreateMovieComponent },
    { path: "admin/createcast", component: CreateCastComponent }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
