import { useLoaderData } from "react-router-dom";

import Card from "../common/Card/Card";
import "./Movies.scss";

const Movies = () => {
  const moviesData = useLoaderData();
  const movies = moviesData.data;

  return (
    <div className="movies">
      <h1 className="movies__title">Movies</h1>
      <div className="movies__content">
        {!moviesData ? (
          <h1>Loading...</h1>
        ) : (
          movies.map((movie) => (
            <Card key={movie.movieId} movie={movie} variant="standard" />
          ))
        )}
      </div>
      <div className="movies__empty"></div>
    </div>
  );
};

export default Movies;
