import {useLoaderData} from "react-router-dom";

import './Movies.scss'
import Card from '../Card/Card'

const Movies = () => {
    const moviesData = useLoaderData();
    
    const bookmarks = moviesData.bookmarks;
    const movies = moviesData.data;
    
    return (
        <div className="movies">
            <h1 className="movies__title">Movies</h1>
            <div className="movies__content">
                {
                    !moviesData ? <h1>Loading...</h1> :
                        movies.map(movie => <Card key={movie.MovieId} movie={movie} bookmarks={bookmarks} />)
                }
            </div>
        </div>
    )
}

export default Movies;