import "./SearchBar.scss";
import {useEffect, useRef, useState} from "react";
import { useNavigate, useLocation } from 'react-router-dom';

const SearchBar = () => {
    const [placeholder, setPlaceholder] = useState("Search for movies or TV series");
    const searchQuery = useRef();
    const navigate = useNavigate();
    const location = useLocation();
    
    useEffect(() => {
        switch(location.pathname) {
            case "/":
                setPlaceholder("Search for movies or TV series");
                break;
            case "/movies":
                setPlaceholder("Search for movies");
                break;
            case "/tv-series":
                setPlaceholder("Search for TV series");
                break;
            case "/bookmarked":
                setPlaceholder("Search for bookmarked movies");
                break;
            default:
                setPlaceholder("Search for movies or TV series");
        }
    }, [location]);
    
    const setBaseUrl = () => {
        let currentUrl = location.pathname;
        const numOfSlashes = currentUrl.split('/').length-1;
        if(numOfSlashes > 1) {
            currentUrl = currentUrl.substring(0, currentUrl.lastIndexOf('/'));
        }
        const currentQuery = searchQuery.current.value;
        if (currentUrl.includes(currentQuery)) {
            currentUrl = currentUrl.replaceAll(currentQuery, "");
        }
        return currentUrl;
    }
    
    const submit = (e) => {
        e.preventDefault();
        const query = searchQuery.current.value;
        const baseUrl = setBaseUrl();
        console.log(baseUrl, "<-- base url");
        console.log(query, "<-- query");
        if(baseUrl.length < 2) {
            navigate(`${query}`, {state: { category: " " }});
        } else {
            navigate(`${baseUrl}/${query}`, {state: { category: baseUrl }});
        }
    }
    
    return (
        <div className="searchBar">
            <img className="searchBar__icon" src="./assets/icon-search.svg" alt="Search icon"/>
            <form className="searchBar__search" onSubmit={submit}>
                <label htmlFor="search" className="visually-hidden">{ placeholder }</label>
                <input className="searchBar__search--input" type="text" ref={ searchQuery } id="search" placeholder={ placeholder }/>
            </form>
        </div>
    )
}

export default SearchBar;