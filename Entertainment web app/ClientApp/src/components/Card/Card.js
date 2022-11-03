import "./Card.scss";

const Card = ({ movie }) => {
    
    const { ImgSmall, Title, Category, Year, Rating } = movie;
    
    return (
        <div className="card">
            <div className="card__img" style={{backgroundImage: 'url(' + ImgSmall + ')'}}>
                <div className="card__img--bookmark">
                    <img src="./assets/icon-bookmark-empty.svg" alt="Bookmark icon"/>
                </div>
            </div>
            <div className="card__info">
                <div className="info">
                    <p className="info__year">{ Year }</p>
                    <span className="info__separator"></span>
                    <p className="info__category">
                        <img className="info__category--img"
                             src={Category === "Movie" ? "./assets/icon-category-movie.svg" : "./assets/icon-category-tv.svg"}
                             alt={Category === "Movie" ? "Icon of movie" : "Icon of TV Series"}/>
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