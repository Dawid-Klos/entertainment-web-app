import "./Trending.scss";
import Card from './TrendingCard/TrendingCard';


const Trending = () => {
    
    return(
        <div className="trending">
            <h1 className="trending__title">Trending</h1>
            <div className="trending__content">
                <Card /> 
                <Card />
                <Card />
                <Card />
            </div>
        </div>
    )
}

export default Trending;