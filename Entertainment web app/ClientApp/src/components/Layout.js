import Header from "./Header/Header";
import {Outlet} from "react-router-dom";
import SearchBar from './SearchBar/SearchBar';
import {useEffect, useState} from "react";

const Layout = () => {

    const [movies, setMovies] = useState([]);
    const [trending, setTrending] = useState([]);
    const [tvSeries, setTvSeries] = useState([]);
    
    const getAllMovies = async () => {
        let fetchAllMovies = await fetch('api/Movies');
        fetchAllMovies = await fetchAllMovies.json();
        setMovies(fetchAllMovies);
    }
    // const getTrendingMovies = async () => {
    //     let fetchTrending = await fetch('api/Movies/GetTrendingMovies');
    //     fetchTrending = await fetchTrending.json();
    //     setTrending(fetchTrending);
    // }
    // const getTvSeries = async () => {
    //     let fetchTvSeries = await fetch('api/Movies/GetTvSeries');
    //     fetchTvSeries = await fetchTvSeries.json();
    //     setTvSeries(fetchTvSeries);
    // }

    useEffect(() => {
        // getTrendingMovies();
        getAllMovies();
        // getTvSeries();
        },[]);
    
    return (
        <>
            <Header />
            <SearchBar />
            <Outlet context={{ movies, setMovies, trending, setTrending, tvSeries, setTvSeries }} />
        </>
    )
}

export default Layout;