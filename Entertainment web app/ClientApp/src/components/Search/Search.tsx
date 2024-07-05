import { useLoaderData } from "react-router-dom";
import { useState, useEffect } from "react";

import Card from "@components/common/Card/Card";
import { Movie } from "@commonTypes/content.types";

import "./Search.scss";

type SearchResult = {
  query: string;
  category: string;
  result: Movie[];
};

const Search = () => {
  const [title, setTitle] = useState<string>("");
  const searchResult = useLoaderData() as SearchResult;

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
            <Card key={movie.movieId} movie={movie} variant="primary" />
          ))
        ) : (
          <p>Content has not been found</p>
        )}
      </div>
    </div>
  );
};

export default Search;
