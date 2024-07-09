import { useLoaderData } from "react-router-dom";

import { Movie } from "@commonTypes/content.types";

import Trending from "./Trending/Trending";
import Recommended from "./Recommended/Recommended";

import "./Home.scss";

type HomeContent = {
  trending: Movie[];
  recommended: Movie[];
};

const Home = () => {
  const homeContent = useLoaderData() as HomeContent;

  return (
    <div className="home">
      <Trending content={homeContent.trending} />
      <Recommended content={homeContent.recommended} />
    </div>
  );
};

export default Home;
