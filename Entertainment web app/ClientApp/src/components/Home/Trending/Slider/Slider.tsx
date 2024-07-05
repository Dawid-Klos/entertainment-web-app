import "keen-slider/keen-slider.min.css";
import { useKeenSlider } from "keen-slider/react";

import Card from "@components/common/Card/Card";
import { Movie } from "@commonTypes/content.types";

import "./Slider.scss";

type SliderProps = {
  movies: Movie[];
};

const Slider = ({ movies }: SliderProps) => {
  const [sliderRef] = useKeenSlider({
    slides: {
      perView: "auto",
      spacing: 20,
    },
    breakpoints: {
      "(min-width: 600px)": {
        slides: {
          perView: "auto",
          spacing: 30,
        },
      },
      "(min-width: 1440px)": {
        slides: {
          perView: "auto",
          spacing: 30,
        },
      },
      "(min-width: 1920px)": {
        slides: {
          perView: "auto",
          spacing: 30,
        },
      },
    },
  });

  return (
    <div ref={sliderRef} className="keen-slider">
      {movies &&
        movies.map((movie) => (
          <div className="keen-slider__slide" key={movie.movieId}>
            <Card movie={movie} variant="secondary" />
          </div>
        ))}
    </div>
  );
};

export default Slider;
