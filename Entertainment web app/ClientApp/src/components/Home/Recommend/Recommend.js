import { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";

import "./Recommend.scss";
import Card from '../../Card/Card';
// import Spinner from '../../../UI/Spinner/Spinner';

const Recommend = () => {

    const [loading, setLoading] = useState(true);
    const { movies } = useOutletContext();

    useEffect(() => {
        if (movies !== undefined) {
            setLoading(false)
        }

    }, [movies]);

    // if (loading) return <Spinner/>;

    return (
        <div className="recommend">
            <h1 className="recommend__title">Recommended for you</h1>
            <div className="recommend__content">
                {
                    movies.map(movie => <Card key={movie.MovieId} movie={movie} />)
                }
            </div>
        </div>
    )
}

export default Recommend;