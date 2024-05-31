import { useLoaderData } from "react-router-dom";

import Card from "../common/Card/Card";
import Spinner from "../common/Spinner/Spinner";
import "./Bookmark.scss";

const Bookmark = () => {
  const bookmarkData = useLoaderData();
  const { tvSeries, movies } = bookmarkData;

  const bookmarkedMovies = (movies.data ||= []);
  const bookmarkedTvSeries = (tvSeries.data ||= []);

  return (
    <section className="bookmark-container">
      <section className="bookmark">
        <h1 className="bookmark__title">Bookmarked Movies</h1>
        <div className="bookmark__content">
          {!bookmarkedMovies || bookmarkedMovies.length < 1 ? (
            <Spinner loading={true} variant="center" />
          ) : (
            movies.data.map((movie) => (
              <Card key={movie.movieId} movie={movie} variant="standard" />
            ))
          )}
        </div>
      </section>
      <section className="bookmark-tv">
        <h2 className="bookmark-tv__title">Bookmarked TV Series</h2>
        <div className="bookmark-tv__content">
          {!bookmarkedTvSeries || bookmarkedTvSeries < 1 ? (
            <Spinner loading={true} variant="center" />
          ) : (
            tvSeries.data.map((movie) => (
              <Card key={movie.movieId} movie={movie} variant="standard" />
            ))
          )}
        </div>
      </section>
    </section>
  );
};

export default Bookmark;
