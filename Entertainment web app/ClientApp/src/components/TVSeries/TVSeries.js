import { useLoaderData } from "react-router-dom";

import Card from "../common/Card/Card";

import "./TVSeries.scss";

const TVSeries = () => {
  const tvSeriesData = useLoaderData();

  const tvSeries = tvSeriesData.data;

  return (
    <div className="TVSeries">
      <h1 className="TVSeries__title">TV Series</h1>
      <div className="TVSeries__content">
        {!tvSeriesData ? (
          <h1>Loading...</h1>
        ) : (
          tvSeries.map((movie) => (
            <Card key={movie.MovieId} movie={movie} variant="standard" />
          ))
        )}
      </div>
    </div>
  );
};

export default TVSeries;
