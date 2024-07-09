import Card from "@components/common/Card/Card";
import Spinner from "@components/common/Spinner/Spinner";
import CarouselIcon from "@assets/icons/Carousel";
import { Movie } from "@commonTypes/content.types";

import "./Recommended.scss";

type RecommendedContent = {
  content: {
    data: Movie[];
  };
};

const Recommended = ({ content }: RecommendedContent) => {
  const movies = (content.data ||= []);

  return (
    <div className="recommend">
      <div className="recommend__title-wrapper">
        <CarouselIcon className="recommend__icon" variant="vertical" />
        <h1 className="recommend__title">Recommended for you</h1>
      </div>

      <div className="recommend__content">
        {!movies || movies.length < 1 ? (
          <Spinner loading={true} variant="primary" />
        ) : (
          movies.map((movie) => (
            <Card key={movie.movieId} movie={movie} variant="primary" />
          ))
        )}
      </div>
    </div>
  );
};

export default Recommended;
