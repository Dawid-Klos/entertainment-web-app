import {useEffect, useState} from "react";
import {useOutletContext} from "react-router-dom";

import './Movies.scss'
import Card from '../Card/Card'
import Spinner from '../../UI/Spinner/Spinner';


const Movies = () => {

    const [loading, setLoading] = useState(true);
    const {movies} = useOutletContext();
    useEffect(() => {
        if (movies !== undefined) {
            setLoading(false)
        }

    }, [movies]);

    return (
        <div className="movies">
            <h1 className="movies__title">Movies</h1>
            <div className="movies__content">
                {
                    loading ? <Spinner /> : (movies.map(movie => <Card key={movie.MovieId} movie={movie}/>))
                }
            </div>
        </div>
    )
}

export default Movies;