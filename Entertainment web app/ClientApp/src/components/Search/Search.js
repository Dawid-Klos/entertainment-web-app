import {useParams, useLocation} from 'react-router-dom';
import {useState, useEffect} from "react";
import './Search.scss';
import Card from "../Card/Card";

const Search = () => {
    const [searchResult, setSearchResult] = useState(null);
    const [title, setTitle] = useState(null);
    const location = useLocation();

    const {query} = useParams();

    const setTitleForQuery = (numberOfResults) => {
        setTitle(`Found ${numberOfResults} results for '${query}'`);
    }
    
    const setCategoryFormat = (category) => {
        switch(category) {
            case "/":
                return " ";
            case "/movies":
                return "Movie";
            case "/tv-series":
                return "TV Series"
            default:
                return " ";
        }
    }

    const getMoviesForQuery = async () => {
        const category = setCategoryFormat(location.state.category);
        let fetchSearchResult = await fetch(`api/Movies/Search?search=${query}&category=${category}`);
        fetchSearchResult = await fetchSearchResult.json();
        setSearchResult(fetchSearchResult);

        const numberOfResults = fetchSearchResult.length;
        setTitleForQuery(numberOfResults, query);
    }

    useEffect(() => {
        getMoviesForQuery();
    }, [query]);

    return (
        <div className="search">
            <h1 className="search__title">{title}</h1>
            <div className="search__content">
                {searchResult ? searchResult.map(movie => <Card key={movie.MovieId} movie={movie}/>) : null}
            </div>
        </div>
    )
}

export default Search;