import { useLoaderData } from "react-router-dom";
import { useState, useEffect } from "react";

import Card from "../common/Card/Card";

import "./Search.scss";

const Search = () => {
  const [title, setTitle] = useState(null);
  const searchResult = useLoaderData();

  const setTitleForQuery = () => {
    setTitle(
      `Found ${searchResult.data.length} results for '${searchResult.query}' in ${searchResult.category}`,
    );
  };

  useEffect(() => {
    if (searchResult) {
      setTitleForQuery();
    }
  }, [searchResult]);

  return (
    <div className="search">
      <h1 className="search__title">{title}</h1>
      <div className="search__content">
        {searchResult ? (
          searchResult.data.map((movie) => (
            <Card key={movie.MovieId} movie={movie} variant="standard" />
          ))
        ) : (
          <p>Content has not been found</p>
        )}
      </div>
    </div>
  );
};

export default Search;
