import "./TVSeries.scss";
import Card from '../Card/Card';

const TVSeries = () => {
    return(
        <div className="TVSeries">
            <h1 className="TVSeries__title">TV Series</h1>
            <div className="TVSeries__content">
                <Card />
                <Card />
                <Card />
                <Card />
            </div>
        </div>
    )
}

export default TVSeries;