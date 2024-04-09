import "keen-slider/keen-slider.min.css";
import { useKeenSlider } from "keen-slider/react";

import Card from "../../../common/Card/Card";

const Slider = ({ movies, bookmarks }) => {
  const [sliderRef] = useKeenSlider({
    slides: {
      perView: 1.25,
      spacing: 20,
    },
    breakpoints: {
      "(min-width: 768px)": {
        slides: {
          perView: 2.25,
          spacing: 30,
        },
      },
      "(min-width: 1440px)": {
        slides: {
          perView: 3.25,
          spacing: 30,
        },
      },
      "(min-width: 1920px)": {
        slides: {
          perView: 3,
          spacing: -80,
        },
      },
    },
  });

  return (
    <div ref={sliderRef} className="keen-slider">
      {movies &&
        movies.map((movie) => (
          <div className="keen-slider__slide" key={movie.MovieId}>
            <Card movie={movie} bookmarks={bookmarks} variant="trending" />
          </div>
        ))}
    </div>
  );
};

export default Slider;
