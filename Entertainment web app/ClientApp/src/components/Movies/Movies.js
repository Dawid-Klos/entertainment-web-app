import './Movies.scss'
import Card from '../Card/Card'

const Movies = () => {
    return(
        <div className="movies">
            <h1 className="movies__title">Movies</h1>
            <div className="movies__content">
                <Card />
                <Card />
                <Card />
                <Card />
            </div>
        </div>
    )
}

export default Movies;