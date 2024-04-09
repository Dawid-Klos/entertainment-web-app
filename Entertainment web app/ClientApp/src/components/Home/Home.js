import { useLoaderData } from "react-router-dom";

import Trending from "./Trending/Trending";
import Recommended from "./Recommended/Recommended";

import "./Home.scss";

const Home = () => {
  const homeContent = useLoaderData();

  return (
    <div className="home">
      <Trending content={homeContent.trending} />
      <Recommended content={homeContent.recommended} />
    </div>
  );
};

export default Home;
