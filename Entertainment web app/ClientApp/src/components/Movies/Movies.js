import './Movies.scss'
import Card from '../Card/Card'
import {useEffect, useState} from "react";
import {useOutletContext} from "react-router-dom";


const Movies = () => {

    const [loading, setLoading] = useState(true);
    const { content } = useOutletContext();
    useEffect(() => {
        if(content !== undefined) {
            setLoading(false)
        }

    }, [content]);
    
    return(
        <div className="movies">
            <h1 className="movies__title">Movies</h1>
            <div className="movies__content">
                {loading ? (<h2>Loading...</h2>) : (
                    content.map(movie => <Card key={movie.movieId} movie={movie}/>)
                )
                }
            </div>
        </div>
    )
}

export default Movies;