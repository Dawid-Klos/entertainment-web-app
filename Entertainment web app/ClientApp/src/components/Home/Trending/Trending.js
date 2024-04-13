import Slider from "./Slider/Slider";

import CarouselIcon from "../../../assets/icons/Carousel";

import "./Trending.scss";

const Trending = ({ content }) => {
  const movies = content.data;
  const bookmarks = content.bookmarks;

  return (
    <div className="trending">
      <div className="trending__title-wrapper">
        <CarouselIcon className="trending__icon" variant="horizontall" />
        <h1 className="trending__title">Trending</h1>
      </div>

      {!content ? (
        <div className="trending__loading">Loading...</div>
      ) : (
        <Slider movies={movies} bookmarks={bookmarks} />
      )}
    </div>
  );
};

export default Trending;
