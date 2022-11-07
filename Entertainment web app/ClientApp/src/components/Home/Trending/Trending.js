import {useOutletContext} from "react-router-dom";
import {useEffect, useState} from "react";

import "./Trending.scss";
import TrendingCard from './TrendingCard/TrendingCard';



const Trending = () => {

    const [loading, setLoading] = useState(true);
    const {trending} = useOutletContext();

    useEffect(() => {
        if (trending !== undefined) {
            setLoading(false)
        }
    }, [trending]);
    
    if (loading) {
        return <></>;
    }
    
    return (<div className="trending">
        <h1 className="trending__title">Trending</h1>
        <div className="trending__content">
            {
                trending.map(movie => <TrendingCard key={movie.MovieId} movie={movie}/>)
            }

        </div>
    </div>)
}

export default Trending;