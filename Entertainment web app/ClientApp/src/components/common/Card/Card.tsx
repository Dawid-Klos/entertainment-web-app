import { Form } from "react-router-dom";

import { useWindowSize } from "@hooks/useWindowSize";
import BookmarkIcon from "@assets/icons/Bookmark";
import { Movie } from "@commonTypes/content.types";

import categoryMovieIcon from "@assets/icon-category-movie.svg";
import categoryTvIcon from "@assets/icon-category-tv.svg";

import "./Card.scss";

type CardProps = {
  movie: Movie;
  variant: "primary" | "secondary";
};

const Card = ({ movie, variant }: CardProps) => {
  const { isMobile } = useWindowSize();
  const backgroundImg = isMobile ? movie.imgSmall : movie.imgLarge;

  return (
    <div className={`${variant === "primary" ? "card" : "trending-card"}`}>
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
