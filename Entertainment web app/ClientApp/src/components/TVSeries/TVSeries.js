import "./TVSeries.scss";
import Card from '../Card/Card';
import {useLoaderData} from "react-router-dom";

const TVSeries = () => {
    const tvSeriesData = useLoaderData();
    
    return (
        <div className="TVSeries">
            <h1 className="TVSeries__title">TV Series</h1>
            <div className="TVSeries__content">
                {
                    !tvSeriesData ? <h1>Loading...</h1> :
                        tvSeriesData.map(movie => <Card key={movie.MovieId} movie={movie} />)
                }
            </div>
        </div>
    )
}

export default TVSeries;