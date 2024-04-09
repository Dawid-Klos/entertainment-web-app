import { useEffect } from "react";

import "keen-slider/keen-slider.min.css";
import { useKeenSlider } from "keen-slider/react";

import Card from "../../../common/Card/Card";

const Slider = ({ movies, bookmarks }) => {
  const [sliderRef, instanceRef] = useKeenSlider({
    slides: {
      perView: 1,
      spacing: 20,
    },
    breakpoints: {
      "(min-width: 600px)": {
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

  useEffect(() => {
    // Indicate that the container is a slider to the user
    // by manually changing the slide
    setTimeout(() => {
      instanceRef.current.moveToIdx(1, true, {
        duration: 1000,
        animation: "easeInOut",
      });
    }, 1000);

    setTimeout(() => {
      instanceRef.current.moveToIdx(0, true, {
        duration: 300,
        animation: "easeInOut",
      });
    }, 2500);
  }, [instanceRef]);

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
