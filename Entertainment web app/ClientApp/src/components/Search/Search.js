import { useLoaderData } from "react-router-dom";
import { useState, useEffect } from "react";

import Card from "../common/Card/Card";

import "./Search.scss";

const Search = () => {
  const [title, setTitle] = useState(null);
  const searchResult = useLoaderData();

  const movies = searchResult.result;

  useEffect(() => {
    setTitle(
      `Found ${movies.length} results for '${searchResult.query}' in ${searchResult.category}`,
    );
  }, [searchResult]);

  return (
    <div className="search">
      <h1 className="search__title">{title}</h1>
      <div className="search__content">
        {movies ? (
          movies.map((movie) => (
            <Card key={movie.movieId} movie={movie} variant="standard" />
          ))
        ) : (
          <p>Content has not been found</p>
        )}
      </div>
    </div>
  );
};

export default Search;
