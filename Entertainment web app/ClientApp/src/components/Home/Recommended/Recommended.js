import Card from "../../common/Card/Card";

import CarouselIcon from "../../../assets/icons/Carousel";

import "./Recommended.scss";

const Recommended = ({ content }) => {
  const movies = content.data;
  const bookmarks = content.bookmarks;

  return (
    <div className="recommend">
      <div className="recommend__title-wrapper">
        <CarouselIcon className="recommend__icon" variant="vertical" />
        <h1 className="recommend__title">Recommended for you</h1>
      </div>

      <div className="recommend__content">
        {!content ? (
          <h1>Loading...</h1>
        ) : (
          movies.map((movie) => (
            <Card
              key={movie.MovieId}
              movie={movie}
              bookmarks={bookmarks}
              variant="standard"
            />
          ))
        )}
      </div>
    </div>
  );
};

export default Recommended;
