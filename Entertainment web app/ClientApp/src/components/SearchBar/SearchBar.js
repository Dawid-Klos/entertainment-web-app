import {useEffect, useState} from "react";
import {useNavigate, useLocation, useSearchParams} from 'react-router-dom';

import { pages } from '../../config/constants';
import searchIcon from '../../assets/icon-search.svg';

import "./SearchBar.scss";


const SearchBar = () => {
    const [placeholder, setPlaceholder] = useState("Search for movies or TV series");
    const [searchParams, setSearchParams] = useSearchParams();
    const navigate = useNavigate();
    const location = useLocation();
    
    const getCurrenPage = () => {
        if(location.pathname === "/") {
            return "Library";
        }
        
        return Object.values(pages).find(page => location.pathname.includes(page.path)).category;
    }
    
    const handleQuery = (e) => {
        const category = getCurrenPage();
        
        const query = e.target.value ? e.target.value : " ";
        const searchQuery = { query: query, category: category };
        
        console.log(searchQuery);
        setSearchParams(searchQuery, { replace: true });
    }

    const navigateToSearch = (e) => {
        e.preventDefault();

        const query = searchParams.get("query");
        const category = searchParams.get("category");

        if(!category) {
            return;
        }

        navigate(`/Search/${category}/${query}`);
    }

    useEffect(() => {
        const placeholder = Object.values(pages).find(page => page.category === getCurrenPage()).placeholder;
        setPlaceholder(placeholder);
        
    }, [location.pathname]);
    
    return (
        <div className="searchBar">
            <img className="searchBar__icon" src={searchIcon} alt="Search icon"/>
            
            <form className="searchBar__search" onSubmit={navigateToSearch}>
                <label htmlFor="search" className="visually-hidden">{ placeholder }</label>
                <input className="searchBar__search--input" type="text" onChange={handleQuery} value={ searchParams.query } id="search" name="search" placeholder={ placeholder }/>
            </form>
            
        </div>
    )
}

export default SearchBar;