import Slider from "./Slider/Slider";

import "./Trending.scss";

const Trending = ({ content }) => {
  const movies = content.data;
  const bookmarks = content.bookmarks;

  return (
    <div className="trending">
      <h1 className="trending__title">Trending</h1>
      {!content ? (
        <div className="trending__loading">Loading...</div>
      ) : (
        <Slider movies={movies} bookmarks={bookmarks} />
      )}
    </div>
  );
};

export default Trending;
