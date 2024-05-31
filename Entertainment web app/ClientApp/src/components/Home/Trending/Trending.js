import Slider from "./Slider/Slider";
import Spinner from "../../common/Spinner/Spinner";
import CarouselIcon from "../../../assets/icons/Carousel";

import "./Trending.scss";

const Trending = ({ content }) => {
  const movies = (content.data ||= []);

  return (
    <div className="trending">
      <div className="trending__title-wrapper">
        <CarouselIcon className="trending__icon" variant="horizontall" />
        <h1 className="trending__title">Trending</h1>
      </div>

      {!movies || movies.length < 1 ? (
        <Spinner loading={true} variant="standard" />
      ) : (
        <Slider movies={movies} />
      )}
    </div>
  );
};

export default Trending;
