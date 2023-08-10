import Trending from "./Trending/Trending";
import Recommended from "./Recommended/Recommended";
import {useLoaderData} from "react-router-dom";

const Home = () => {
    const homeContent = useLoaderData();
    
    return(
        <>
            <Trending movies={homeContent.trending} />
            <Recommended movies={homeContent.recommended} />
        </>
    )

}

export default Home;