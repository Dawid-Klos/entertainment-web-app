import Header from "./Header/Header";
import {Outlet} from "react-router-dom";
import SearchBar from './SearchBar/SearchBar';
import {useEffect, useState} from "react";

const Layout = () => {

    const [content, setContent] = useState();
    const [trending, setTrending] = useState();
    // const [tvSeries, setTvSeries] = useState([]);
    
    const getAllContent = async () => {
        let fetchAllContent = await fetch('api/movies');
        fetchAllContent = await fetchAllContent.json();
        setContent(fetchAllContent);
    }
    const getTrendingMovies = async () => {
        let fetchTrending = await fetch('api/movies/GetTrendingMovies');
        fetchTrending = await fetchTrending.json();
        setTrending(fetchTrending);
    }

    useEffect(() => {
        getAllContent();
        getTrendingMovies();
        },[]);
    
    return (
        <>
            <Header />
            <SearchBar />
            <Outlet context={{ content, setContent, trending, setTrending }} />
        </>
    )
}

export default Layout;