import { useLoaderData } from "react-router-dom";

import Card from "../common/Card/Card";
import "./Bookmark.scss";

const Bookmark = () => {
  const bookmarkData = useLoaderData();

  const bookmarks = bookmarkData.bookmarks;
  const movies = bookmarkData.data;

  const filterByCategory = (data, category) => {
    return data.filter((movie) => movie.Category === category);
  };

  return (
    <section className="bookmark-container">
      <section className="bookmark">
        <h1 className="bookmark__title">Bookmarked Movies</h1>
        <div className="bookmark__content">
          {!bookmarkData ? (
            <h1>Loading...</h1>
          ) : (
            filterByCategory(movies, "Movies").map((movie) => (
              <Card
                key={movie.MovieId}
                movie={movie}
                bookmarks={bookmarks}
                variant="standard"
              />
            ))
          )}
        </div>
      </section>
      <section className="bookmark-tv">
        <h2 className="bookmark-tv__title">Bookmarked TV Series</h2>
        <div className="bookmark-tv__content">
          {!bookmarkData ? (
            <h1>Loading...</h1>
          ) : (
            filterByCategory(movies, "TV Series").map((movie) => (
              <Card
                key={movie.MovieId}
                movie={movie}
                bookmarks={bookmarks}
                variant="standard"
              />
            ))
          )}
        </div>
      </section>
    </section>
  );
};

export default Bookmark;
