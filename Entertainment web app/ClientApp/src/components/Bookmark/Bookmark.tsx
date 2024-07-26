import { useLoaderData } from "react-router-dom";

import Card from "@components/common/Card/Card";
import { Movie } from "@commonTypes/content.types";

import "./Bookmark.scss";

type BookmarkData = {
  tvSeries: {
    data: Movie[];
  };
  movies: {
    data: Movie[];
  };
};

const Bookmark = () => {
  const content = useLoaderData() as BookmarkData;

  const bookmarkedMovies = (content.movies.data ||= []);
  const bookmarkedTvSeries = (content.tvSeries.data ||= []);

  return (
    <section className="bookmark-container">
      <section className="bookmark">
        <h1 className="bookmark__title">Bookmarked Movies</h1>
        <div className="bookmark__content">
          {bookmarkedMovies.length === 0 ? (
            <p className="bookmark__empty">
              You have not bookmarked any movies yet.
            </p>
          ) : (
            bookmarkedMovies.map((movie) => (
              <Card key={movie.movieId} movie={movie} variant="primary" />
            ))
          )}
        </div>
      </section>
      <section className="bookmark-tv">
        <h2 className="bookmark-tv__title">Bookmarked TV Series</h2>
        <div className="bookmark-tv__content">
          {bookmarkedTvSeries.length === 0 ? (
            <p className="bookmark-tv__empty">
              You have not bookmarked any TV series yet.
            </p>
          ) : (
            bookmarkedTvSeries.map((movie) => (
              <Card key={movie.movieId} movie={movie} variant="primary" />
            ))
          )}
        </div>
      </section>
    </section>
  );
};

export default Bookmark;
