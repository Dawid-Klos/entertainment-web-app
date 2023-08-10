import "./Trending.scss";
import TrendingCard from './TrendingCard/TrendingCard';

const Trending = ({ movies }) => {
    
    return (<div className="trending">
        <h1 className="trending__title">Trending</h1>
        <div className="trending__content">
            {
                !movies ? <div className="trending__loading">Loading...</div> :
                    movies.map(movie => <TrendingCard key={movie.MovieId} movie={movie}/>)
            }

        </div>
    </div>)
}

export default Trending;