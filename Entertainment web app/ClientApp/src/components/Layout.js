import Header from "./Header/Header";
import {Outlet} from "react-router-dom";
import SearchBar from './SearchBar/SearchBar';
import {useEffect, useState} from "react";
import { useIsAuthenticated} from "react-auth-kit";

const Layout = () => {

    const [movies, setMovies] = useState([]);
    const [trending, setTrending] = useState([]);
    const [tvSeries, setTvSeries] = useState([]);
    const isAuthenticated = useIsAuthenticated();
    
    const getHeaders = () => {
        const myHeaders = new Headers();
        myHeaders.append('Content-Type', 'application/json');
        
        return myHeaders;
    }
    
    const getAllMovies = async () => {
        let fetchAllMovies = await fetch('api/Movies/GetAllMovies', {
            method: 'GET',
            headers: getHeaders()
        });
        
        fetchAllMovies = await fetchAllMovies.json();
        setMovies(fetchAllMovies);
    }
    
    const getTrendingMovies = async () => {
        let fetchTrending = await fetch('api/Movies/GetTrendingMovies', {
            method: 'GET',
            headers: getHeaders()
        });
        
        fetchTrending = await fetchTrending.json();
        
        if (fetchTrending.isSuccess) {
            setTrending(fetchTrending);
        }
    }
    const getTvSeries = async () => {
        let fetchTvSeries = await fetch('api/Movies/GetTvSeries',{
            method: 'GET',
            headers: getHeaders()
        });
        
        fetchTvSeries = await fetchTvSeries.json();
        setTvSeries(fetchTvSeries);
    }

    useEffect(() => {
        // check if auth
        if (isAuthenticated()) {
            console.log("Authenticated");
        } else {
            console.log("Not authenticated");
        }
        
        getTrendingMovies();
        getAllMovies();
        getTvSeries();
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