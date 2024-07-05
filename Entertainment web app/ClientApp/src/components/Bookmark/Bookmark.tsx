import { useLoaderData } from "react-router-dom";

import Card from "@components/common/Card/Card";
import Spinner from "@components/common/Spinner/Spinner";
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
  const bookmarkData = useLoaderData() as BookmarkData;
  const { tvSeries, movies } = bookmarkData;

  const bookmarkedMovies = (movies.data ||= []);
  const bookmarkedTvSeries = (tvSeries.data ||= []);

  return (
    <section className="bookmark-container">
      <section className="bookmark">
        <h1 className="bookmark__title">Bookmarked Movies</h1>
        <div className="bookmark__content">
          {!bookmarkedMovies || bookmarkedMovies.length < 1 ? (
            <Spinner loading={true} variant="primary" />
          ) : (
            movies.data.map((movie) => (
              <Card key={movie.movieId} movie={movie} variant="primary" />
            ))
          )}
        </div>
      </section>
      <section className="bookmark-tv">
        <h2 className="bookmark-tv__title">Bookmarked TV Series</h2>
        <div className="bookmark-tv__content">
          {!bookmarkedTvSeries || bookmarkedTvSeries.length < 1 ? (
            <Spinner loading={true} variant="primary" />
          ) : (
            tvSeries.data.map((movie) => (
              <Card key={movie.movieId} movie={movie} variant="primary" />
            ))
          )}
        </div>
      </section>
    </section>
  );
};

export default Bookmark;
