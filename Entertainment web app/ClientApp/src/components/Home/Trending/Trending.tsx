import Spinner from "@components/common/Spinner/Spinner";
import CarouselIcon from "@assets/icons/Carousel";
import { Movie } from "@commonTypes/content.types";

import Slider from "./Slider/Slider";
import "./Trending.scss";

type TrendingContent = {
  content: {
    data: Movie[];
  };
};

const Trending = ({ content }: TrendingContent) => {
  const movies = (content.data ||= []);

  return (
    <div className="trending">
      <div className="trending__title-wrapper">
        <CarouselIcon className="trending__icon" variant="horizontal" />
        <h1 className="trending__title">Trending</h1>
      </div>

      {!movies || movies.length < 1 ? (
        <Spinner loading={true} variant="primary" />
      ) : (
        <Slider movies={movies} />
      )}
    </div>
  );
};

export default Trending;
