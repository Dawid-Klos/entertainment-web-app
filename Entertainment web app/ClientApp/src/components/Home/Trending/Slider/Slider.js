import "keen-slider/keen-slider.min.css";
import { useKeenSlider } from "keen-slider/react";

import Card from "../../../common/Card/Card";

const Slider = ({ movies, bookmarks }) => {
  const [sliderRef] = useKeenSlider({
    slides: {
      perView: 2,
      spacing: 10,
    },
  });

  // TODO: Implement the breakpoint for the slider

  return (
    <div ref={sliderRef} className="keen-slider">
      {movies &&
        movies.map((movie) => (
          <div className="keen-slider__slide">
            <Card
              key={movie.MovieId}
              movie={movie}
              bookmarks={bookmarks}
              variant="trending"
            />
          </div>
        ))}
    </div>
  );
};

export default Slider;
