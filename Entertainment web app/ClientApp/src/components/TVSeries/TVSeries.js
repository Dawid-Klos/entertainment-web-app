import {useEffect, useState} from "react";
import {useOutletContext} from "react-router-dom";

import "./TVSeries.scss";
import Card from '../Card/Card';
import Spinner from '../../UI/Spinner/Spinner';

const TVSeries = () => {

    const [loading, setLoading] = useState(true);
    const {tvSeries} = useOutletContext();

    useEffect(() => {
        if (tvSeries !== undefined) {
            setLoading(false)
        }

    }, [tvSeries]);
    
    if (loading) return <Spinner/>

    return (
        <div className="TVSeries">
            <h1 className="TVSeries__title">TV Series</h1>
            <div className="TVSeries__content">
                {
                    tvSeries.map(movie => <Card key={movie.MovieId} movie={movie}/>)
                }
            </div>
        </div>
    )
}

export default TVSeries;