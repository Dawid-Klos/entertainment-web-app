import "./TVSeries.scss";
import Card from '../Card/Card';
import {useEffect, useState} from "react";
import {useOutletContext} from "react-router-dom";

const TVSeries = () => {

    const [loading, setLoading] = useState(true);
    const { tvSeries } = useOutletContext();

    useEffect(() => {
        if(tvSeries !== undefined) {
            setLoading(false)
        }

    }, [tvSeries]);
    
    return(
        <div className="TVSeries">
            <h1 className="TVSeries__title">TV Series</h1>
            <div className="TVSeries__content">
                {loading ? (<h2>Loading...</h2>) : (
                    tvSeries.map(movie => <Card key={ movie.MovieId } movie={ movie }/>)
                )
                }
            </div>
        </div>
    )
}

export default TVSeries;