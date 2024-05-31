import { useLoaderData } from "react-router-dom";

import Card from "../common/Card/Card";
import Spinner from "../common/Spinner/Spinner";
import "./TVSeries.scss";

const TVSeries = () => {
  const tvSeriesData = useLoaderData();
  const tvSeries = (tvSeriesData.data ||= []);

  return (
    <div className="TVSeries">
      <h1 className="TVSeries__title">TV Series</h1>
      <div className="TVSeries__content">
        {!tvSeries || tvSeries.length < 1 ? (
          <Spinner loading={true} variant="center" />
        ) : (
          tvSeries.map((movie) => (
            <Card key={movie.movieId} movie={movie} variant="standard" />
          ))
        )}
      </div>
    </div>
  );
};

export default TVSeries;
