import Slider from "./Slider/Slider";

import carouselIcon from "../../../assets/carousel-horizontal.svg";
import "./Trending.scss";

const Trending = ({ content }) => {
  const movies = content.data;
  const bookmarks = content.bookmarks;

  return (
    <div className="trending">
      <div className="trending__title-wrapper">
        <h1 className="trending__title">Trending</h1>

        <svg
          className="trending__icon"
          xmlns="http://www.w3.org/2000/svg"
          width={24}
          height={24}
          viewBox="0 0 24 24"
          fill="none"
          stroke="currentColor"
          strokeWidth={2}
          strokeLinecap="round"
          strokeLinejoin="round"
        >
          <path stroke="none" d="M0 0h24v24H0z" fill="none" />
          <path d="M7 5m0 1a1 1 0 0 1 1 -1h8a1 1 0 0 1 1 1v12a1 1 0 0 1 -1 1h-8a1 1 0 0 1 -1 -1z" />
          <path d="M22 17h-1a1 1 0 0 1 -1 -1v-8a1 1 0 0 1 1 -1h1" />
          <path d="M2 17h1a1 1 0 0 0 1 -1v-8a1 1 0 0 0 -1 -1h-1" />
        </svg>
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
