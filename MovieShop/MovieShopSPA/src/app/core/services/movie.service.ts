import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from 'src/app/shared/models/movie';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  // talk with movie API
  constructor(private apiService: ApiService) { }

  // Called by MOvie Details Component
  getMovieDetails(id: number): Observable<Movie> {
    return this.apiService.getOne(`${'movies/'}`, id);
  }

  getTopRevenueMovies(): Observable<MovieCard[]> {

    return this.apiService.getList('movies/toprevenue')

  }

  getMoviesByGenre(id: number): Observable<MovieCard[]> {

    return this.apiService.getList(`${'movies/genre/'}${id}`)

  }
}
