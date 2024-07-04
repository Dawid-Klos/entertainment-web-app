import { Form } from "react-router-dom";

import { useWindowSize } from "@hooks/useWindowSize";
import BookmarkIcon from "@assets/icons/Bookmark";

import categoryMovieIcon from "@assets/icon-category-movie.svg";
import categoryTvIcon from "@assets/icon-category-tv.svg";

import "./Card.scss";

type CardProps = {
  movie: {
    movieId: string;
    imgSmall: string;
    imgLarge: string;
    year: number;
    category: string;
    rating: number;
    title: string;
    isBookmarked: string;
  };
  variant: "trending" | "normal";
};

const Card = ({ movie, variant }: CardProps) => {
  const { isMobile } = useWindowSize();
  const backgroundImg = isMobile ? movie.imgSmall : movie.imgLarge;

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
        <Form className="card-bookmark__form" method="post">
          <input type="hidden" name="movieId" value={movie.movieId} />
          <input
            type="hidden"
            name="isBookmarked"
            value={movie.isBookmarked ? movie.isBookmarked.toString() : "false"}
          />
          <button
            className="card-bookmark__button"
            aria-label="Adds a movie to bookmarks"
          >
            <BookmarkIcon
              className="card-bookmark__icon"
              variant={movie.isBookmarked ? "filled" : "outlined"}
            />
          </button>
        </Form>
      </div>
      <div className="card-bottom">
        <div className="card-info">
          <p>{movie.year}</p>
          <span className="card-info__separator"></span>
          <p className="card-info__category">
            <img
              src={
                movie.category === "Movie" ? categoryMovieIcon : categoryTvIcon
              }
              alt={movie.category === "Movie" ? "Movie" : "TV Series"}
            />
            {movie.category}
          </p>
          <span className="card-info__separator"></span>
          <p>{movie.rating}</p>
        </div>
        <h2 className="card-bottom__title">{movie.title}</h2>
      </div>
    </div>
  );
};

export default Card;
