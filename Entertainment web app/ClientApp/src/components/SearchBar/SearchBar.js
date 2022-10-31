import "./SearchBar.scss";

const SearchBar = () => {

    return (
        <div className="searchBar">
            <img className="searchBar__icon" src="./assets/icon-search.svg" alt="Search icon"/>
            <form className="searchBar__search">
                <label htmlFor="search" className="visually-hidden">Search for movies or TV series</label>
                <input className="searchBar__search--input" type="text" id="search" placeholder="Search for movies or TV series"/>
            </form>
        </div>
    )
}

export default SearchBar;