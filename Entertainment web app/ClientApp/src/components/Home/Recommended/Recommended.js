import "./Recommended.scss";
import Card from '../../Card/Card';

const Recommended = ({ movies }) => {
    
    return (
        <div className="recommend">
            <h1 className="recommend__title">Recommended for you</h1>
            <div className="recommend__content">
                {
                    !movies ? <h1>Loading...</h1> :
                        movies.map(movie => <Card key={movie.MovieId} movie={movie} />)
                }
            </div>
        </div>
    )
}

export default Recommended;