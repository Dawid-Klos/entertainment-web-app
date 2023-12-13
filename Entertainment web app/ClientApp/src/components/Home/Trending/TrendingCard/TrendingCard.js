import { useWindowSize } from "../../../../hooks/useWindowSize";

import categoryMovieIcon from "../../../../assets/icon-category-movie.svg";
import categoryTvIcon from "../../../../assets/icon-category-tv.svg";
import bookmarkIcon from "../../../../assets/icon-bookmark-empty.svg";

import "./TrendingCard.scss";

const TrendingCard = ({ movie }) => {
  const { isMobile } = useWindowSize();
  const { ImgTrendingSmall, ImgTrendingLarge, Movie } = movie;

  const backgroundImg = isMobile ? ImgTrendingSmall : ImgTrendingLarge;
  const backgroundStyles = {
    backgroundImage: "url(" + backgroundImg + ")",
    backgroundSize: "cover",
    backgroundPosition: "center",
    backgroundRepeat: "no-repeat",
  };

  return (
    <div className="trending-card" style={backgroundStyles}>
      <div className="trending-card__info">
        <div className="trending-info">
          <p className="trending-info__year">{Movie.Year}</p>
          <span className="trending-info__separator"></span>
          <p className="trending-info__category">
            <img
              className="trending-info__category--img"
              src={
                Movie.Category === "Movie" ? categoryMovieIcon : categoryTvIcon
              }
              alt={Movie.Category === "Movie" ? "Movie" : "TV Series"}
            />
            {Movie.Category}
          </p>
          <span className="trending-info__separator"></span>
          <p className="trending-info__rating">{Movie.Rating}</p>
        </div>
        <h2 className="trending-card__info--title">{Movie.Title}</h2>
      </div>

      <div className="trending-card__bookmark">
        <img
          className="trending-card__bookmark--img"
          src={bookmarkIcon}
          alt="Bookmark icon"
        />
      </div>
    </div>
  );
};

export default TrendingCard;
