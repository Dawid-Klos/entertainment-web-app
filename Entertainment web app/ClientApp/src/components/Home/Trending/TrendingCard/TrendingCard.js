import "./TrendingCard.scss";

const image = "./assets/thumbnails/1998/trending/small.jpg";

const TrendingCard = ({movie}) => {

    const {ImgTrendingSmall, Title, Year, Rating, Category} = movie;

    return (
        <div className="trending-card" style={{backgroundImage: 'url(' + ImgTrendingSmall + ')'}}>
            <div className="trending-card__info">
                <div className="trending-info">
                    <p className="trending-info__year">{Year}</p>
                    <span className="trending-info__separator"></span>
                    <p className="trending-info__category">
                        <img className="trending-info__category--img"
                             src={Category === "Movie" ? "./assets/icon-category-movie.svg" : "./assets/icon-category-tv.svg"}
                             alt={Category === "Movie" ? "Icon of movie" : "Icon of TV Series"}/>
                        {Category}
                    </p>
                    <span className="trending-info__separator"></span>
                    <p className="trending-info__rating">{Rating}</p>
                </div>
                <h2 className="trending-card__info--title">{Title}</h2>
            </div>

            <div className="trending-card__bookmark">
                <img className="trending-card__bookmark--img" src="./assets/icon-bookmark-empty.svg"
                     alt="Bookmark icon"/>
            </div>
        </div>
    )
}

export default TrendingCard;