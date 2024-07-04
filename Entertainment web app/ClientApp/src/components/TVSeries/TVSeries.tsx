import { useLoaderData } from "react-router-dom";

import Card from "@components/common/Card/Card";
import Spinner from "@components/common/Spinner/Spinner";
import "./TVSeries.scss";

type TVSeriesData = {
  data: {
    movieId: string;
    imgSmall: string;
    imgLarge: string;
    year: number;
    category: string;
    rating: number;
    title: string;
    isBookmarked: string;
  }[];
};

const TVSeries = () => {
  const tvSeriesData = useLoaderData() as TVSeriesData;
  const tvSeries = (tvSeriesData.data ||= []);

  return (
    <div className="TVSeries">
      <h1 className="TVSeries__title">TV Series</h1>
      <div className="TVSeries__content">
        {!tvSeries || tvSeries.length < 1 ? (
          <Spinner loading={true} variant="primary" />
        ) : (
          tvSeries.map((movie) => (
            <Card key={movie.movieId} movie={movie} variant="primary" />
          ))
        )}
      </div>
    </div>
  );
};

export default TVSeries;
