import { useEffect } from "react";

import { useWindowSize } from "../../../hooks/useWindowSize";
import useBookmark from "../../../hooks/useBookmark";
import Bookmark from "../../../assets/icons/Bookmark";

import categoryMovieIcon from "../../../assets/icon-category-movie.svg";
import categoryTvIcon from "../../../assets/icon-category-tv.svg";

import "./Card.scss";

const Card = ({ movie, bookmarks, variant }) => {
  const { isMobile } = useWindowSize();
  const { cardInfo, setCardInfo, handleBookmark } = useBookmark();

  useEffect(() => {
    if (!movie || !bookmarks) {
      return;
    }

    if (variant === "trending") {
      const { ImgTrendingSmall, ImgTrendingLarge, Movie } = movie;

      setCardInfo({
        id: Movie.MovieId,
        title: Movie.Title,
        year: Movie.Year,
        category: Movie.Category,
        rating: Movie.Rating,
        imgSmall: ImgTrendingSmall,
        imgLarge: ImgTrendingLarge,
        isBookmarked: bookmarks.includes(Movie.MovieId),
      });
    }

    if (variant === "standard") {
      const { MovieId, ImgSmall, ImgLarge, Title, Category, Year, Rating } =
        movie;

      setCardInfo({
        id: MovieId,
        title: Title,
        year: Year,
        category: Category,
        rating: Rating,
        imgSmall: ImgSmall,
        imgLarge: ImgLarge,
        isBookmarked: bookmarks.includes(MovieId),
      });
    }
  }, [movie, bookmarks]);

  const backgroundImg = isMobile ? cardInfo.imgSmall : cardInfo.imgLarge;

  return (
    <div className={`${variant === "trending" ? "trending-card" : "card"}`}>
      <div
        className="card-bookmark"
        style={{
          backgroundImage: "url(" + backgroundImg + ")",
          backgroundSize: "cover",
          backgroundPosition: "center",
          backgroundRepeat: "no-repeat",
        }}
      >
        <button
          onClick={handleBookmark}
          className="card-bookmark__button"
          aria-label="Adds a movie to bookmarks"
        >
          <Bookmark
            className="card-bookmark__icon"
            variant={cardInfo.isBookmarked ? "filled" : "outlined"}
          />
        </button>
      </div>
      <div className="card-bottom">
        <div className="card-info">
          <p>{cardInfo.year}</p>
          <span className="card-info__separator"></span>
          <p className="card-info__category">
            <img
              src={
                cardInfo.category === "Movie"
                  ? categoryMovieIcon
                  : categoryTvIcon
              }
              alt={cardInfo.category === "Movie" ? "Movie" : "TV Series"}
            />
            {cardInfo.category}
          </p>
          <span className="card-info__separator"></span>
          <p>{cardInfo.rating}</p>
        </div>
        <h2 className="card-bottom__title">{cardInfo.title}</h2>
      </div>
    </div>
  );
};

export default Card;
