import { useLoaderData } from "react-router-dom";

import Trending from "./Trending/Trending";
import Recommended from "./Recommended/Recommended";

const Home = () => {
  const homeContent = useLoaderData();

  return (
    <>
      <Trending content={homeContent.trending} />
      <Recommended content={homeContent.recommended} />
    </>
  );
};

export default Home;
