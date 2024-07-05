import { useSearch } from "@hooks/useSearch";

import searchIcon from "@assets/icon-search.svg";
import "./SearchBar.scss";

const SearchBar = () => {
  const { pageInfo, navigateToSearch } = useSearch();

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
        />
      </form>
    </div>
  );
};

export default SearchBar;
