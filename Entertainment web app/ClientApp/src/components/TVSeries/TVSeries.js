import { useLoaderData } from "react-router-dom";

import Card from "../Card/Card";

import "./TVSeries.scss";

const TVSeries = () => {
  const tvSeriesData = useLoaderData();

  const tvSeries = tvSeriesData.data;
  const bookmarks = tvSeriesData.bookmarks;

  return (
    <div className="TVSeries">
      <h1 className="TVSeries__title">TV Series</h1>
      <div className="TVSeries__content">
        {!tvSeriesData ? (
          <h1>Loading...</h1>
        ) : (
          tvSeries.map((movie) => (
            <Card key={movie.MovieId} movie={movie} bookmarks={bookmarks} />
          ))
        )}
      </div>
    </div>
  );
};

export default TVSeries;
