import Card from "../../common/Card/Card";
import Spinner from "../../common/Spinner/Spinner";
import CarouselIcon from "../../../assets/icons/Carousel";

import "./Recommended.scss";

const Recommended = ({ content }) => {
  const movies = (content.data ||= []);

  return (
    <div className="recommend">
      <div className="recommend__title-wrapper">
        <CarouselIcon className="recommend__icon" variant="vertical" />
        <h1 className="recommend__title">Recommended for you</h1>
      </div>

      <div className="recommend__content">
        {!movies || movies.length < 1 ? (
          <Spinner loading={true} variant="center" />
        ) : (
          movies.map((movie) => (
            <Card key={movie.movieId} movie={movie} variant="standard" />
          ))
        )}
      </div>
    </div>
  );
};

export default Recommended;
