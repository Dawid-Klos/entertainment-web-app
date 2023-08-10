import {useLoaderData} from "react-router-dom";

import './Movies.scss'
import Card from '../Card/Card'

const Movies = () => {
    const moviesData = useLoaderData();
    
    return (
        <div className="movies">
            <h1 className="movies__title">Movies</h1>
            <div className="movies__content">
                {
                    !moviesData ? <h1>Loading...</h1> :
                        moviesData.map(movie => <Card key={movie.MovieId} movie={movie}/>)
                }
            </div>
        </div>
    )
}

export default Movies;