import Card from "../../common/Card/Card";

import "./Trending.scss";

const Trending = ({ content }) => {
  const movies = content.data;
  const bookmarks = content.bookmarks;

  return (
    <div className="trending">
      <h1 className="trending__title">Trending</h1>
      <div className="trending__content">
        {!content ? (
          <div className="trending__loading">Loading...</div>
        ) : (
          movies.map((movie) => (
            <Card
              key={movie.MovieId}
              movie={movie}
              bookmarks={bookmarks}
              variant="trending"
            />
          ))
        )}
      </div>
    </div>
  );
};

export default Trending;
