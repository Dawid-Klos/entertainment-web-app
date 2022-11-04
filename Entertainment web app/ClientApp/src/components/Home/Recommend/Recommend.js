import "./Recommend.scss";
import Card from '../../Card/Card';
import {useEffect, useState} from "react";
import {useOutletContext} from "react-router-dom";

const Recommend = () => {

    const [loading, setLoading] = useState(true);
    const { movies } = useOutletContext();

    useEffect(() => {
        if(movies !== undefined) {
            setLoading(false)
        }

    }, [movies]);
    
    return(
        <div className="recommend">
            <h1 className="recommend__title">Recommended for you</h1>
            <div className="recommend__content">
                {loading ? (<h2>Loading...</h2>) : (
                    movies.map(movie => <Card key={movie.MovieId} movie={movie}/>)
                )
                }
            </div>
        </div>
    )
}

export default Recommend;