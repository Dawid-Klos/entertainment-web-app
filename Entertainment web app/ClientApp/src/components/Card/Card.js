import bookmarkIcon from "../../assets/icon-bookmark-empty.svg";
import categoryMovieIcon from "../../assets/icon-category-movie.svg";
import categoryTvIcon from "../../assets/icon-category-tv.svg";

import "./Card.scss";

const Card = ({ movie }) => {
    
    const { ImgSmall, Title, Category, Year, Rating } = movie;
    
    return (
        <div className="card">
            <div className="card__img" style={{
                background: 'url(' + ImgSmall + ')',
                backgroundSize: 'cover',
                backgroundPosition: 'center',
                backgroundRepeat: 'no-repeat'
            }}>
                <div className="card__img--bookmark">
                    <img src={bookmarkIcon} alt="Bookmark icon"/>
                </div>
            </div>
            <div className="card__info">
                <div className="info">
                    <p className="info__year">{ Year }</p>
                    <span className="info__separator"></span>
                    <p className="info__category">
                        <img className="info__category--img"
                             src={Category === "Movie" ? categoryMovieIcon : categoryTvIcon}
                             alt={Category === "Movie" ? "Movie" : "TV Series"} />
                        { Category }
                    </p>
                    <span className="info__separator"></span>
                    <p className="info__rating">{ Rating }</p>
                </div>
                <h2 className="card__info--title">{ Title }</h2>
            </div>
        </div>
    )
}

export default Card;