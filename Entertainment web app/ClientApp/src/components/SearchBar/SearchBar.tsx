import { useSearch } from "@hooks/useSearch";

import searchIcon from "@assets/icon-search.svg";
import "./SearchBar.scss";

const SearchBar = () => {
  const { pageInfo, query, setQuery, navigateToSearch } = useSearch();

  const onChange: React.FormEventHandler<HTMLInputElement> = (e) => {
    e.preventDefault();
    setQuery(e.currentTarget.value);
    localStorage.setItem("query", e.currentTarget.value);
  };

  return (
    <div className="searchBar">
      <img className="searchBar__icon" src={searchIcon} alt="Search icon" />

      <form className="searchBar__search" onSubmit={navigateToSearch}>
        <label htmlFor="search" className="visually-hidden">
          {pageInfo.placeholder}
        </label>
        <input
          className="searchBar__search--input"
          type="search"
          id="search"
          name="search"
          placeholder={pageInfo.placeholder}
          autoComplete="off"
          value={query || localStorage.getItem("query") || ""}
          onChange={(e) => onChange(e)}
        />
      </form>
    </div>
  );
};

export default SearchBar;
