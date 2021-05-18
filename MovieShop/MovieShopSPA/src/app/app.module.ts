import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GenresComponent } from './genres/genres.component';
import { HeaderComponent } from './core/layout/header/header.component';
import { FooterComponent } from './core/layout/footer/footer.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { CreateMovieComponent } from './auth/create-movie/create-movie.component';
import { CreateCastComponent } from './auth/create-cast/create-cast.component';
import { HomeComponent } from './home/home.component';
import { MovieCardListComponent } from './movies/movie-card-list/movie-card-list.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { FavoriteButtonComponent } from './shared/Components/favorite-button/favorite-button.component';
import { MovieCardComponent } from './shared/Components/movie-card/movie-card.component';
import { NotFoundComponent } from './shared/Components/not-found/not-found.component';


@NgModule({
  declarations: [
    AppComponent,
    GenresComponent,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    RegisterComponent,
    CreateMovieComponent,
    CreateCastComponent,
    HomeComponent,
    MovieCardListComponent,
    MovieCardComponent,
    FavoriteButtonComponent,
    NotFoundComponent,
    MovieDetailsComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
