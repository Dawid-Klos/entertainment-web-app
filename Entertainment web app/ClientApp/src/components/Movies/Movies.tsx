import { useLoaderData } from "react-router-dom";

import Card from "@components/common/Card/Card";
import Spinner from "@components/common/Spinner/Spinner";
import { Movie } from "@commonTypes/content.types";

import "./Movies.scss";

type MoviesData = {
  data: Movie[];
};

const Movies = () => {
  const moviesData = useLoaderData() as MoviesData;
  const movies = (moviesData.data ||= []);

  return (
    <div className="movies">
      <h1 className="movies__title">Movies</h1>
      <div className="movies__content">
        {!movies || movies.length < 1 ? (
          <Spinner loading={true} variant="primary" />
        ) : (
          movies.map((movie) => (
            <Card key={movie.movieId} movie={movie} variant="primary" />
          ))
        )}
      </div>
      <div className="movies__empty"></div>
    </div>
  );
};

export default Movies;
