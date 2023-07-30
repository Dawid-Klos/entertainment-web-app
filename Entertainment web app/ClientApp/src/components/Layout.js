import Header from "./Header/Header";
import {Outlet} from "react-router-dom";
import SearchBar from './SearchBar/SearchBar';
import {useEffect, useState} from "react";

import { useCookies } from 'react-cookie';
const Layout = () => {

    const [movies, setMovies] = useState([]);
    const [trending, setTrending] = useState([]);
    const [tvSeries, setTvSeries] = useState([]);
    const [cookies, setCookie] = useCookies(['_auth']);
    
    const getHeaders = () => {
        const token = cookies._auth;
        
        const myHeaders = new Headers();
        myHeaders.append('Accept', 'application/json');
        myHeaders.append('withCredentials', 'true');
        myHeaders.append('Content-Type', 'application/json');
        myHeaders.append('Authorization', `Bearer ${token}`)
        
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
        setTrending(fetchTrending);
    }
    const getTvSeries = async () => {
        let fetchTvSeries = await fetch('api/Movies/GetTvSeries',{
            method: 'GET',
            headers: getHeaders()
        });
        
        fetchTvSeries = await fetchTvSeries.json();
        console.log(fetchTvSeries.length);
        setTvSeries(fetchTvSeries);
    }

    useEffect(() => {
        
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