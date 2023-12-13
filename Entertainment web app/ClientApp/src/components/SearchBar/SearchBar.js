import { useEffect, useState } from "react";
import { useNavigate, useLocation, useSearchParams } from "react-router-dom";

import { pages } from "../../config/constants";
import searchIcon from "../../assets/icon-search.svg";

import "./SearchBar.scss";

const SearchBar = () => {
  const [pageInfo, setPageInfo] = useState(pages.home);
  const [searchParams, setSearchParams] = useSearchParams();

  const navigate = useNavigate();
  const location = useLocation();

  const handleQuery = (e) => {
    const query = e.target.value ? e.target.value : " ";
    const searchQuery = { query: query, category: pageInfo.category };

    setSearchParams(searchQuery, { replace: true });
  };

  const navigateToSearch = (e) => {
    e.preventDefault();

    let query = searchParams.get("query");

    if (!query) {
      query = "";
    }

    console.log("Search category", pageInfo.path);

    navigate(`/Search${pageInfo.path}/${query}`);
  };

  useEffect(() => {
    let currentPage = Object.values(pages).find((page) =>
      location.pathname.includes(page.path),
    );
    currentPage = currentPage ? currentPage : pages.home;

    setPageInfo(currentPage);
  }, [location.pathname]);

  return (
    <div className="searchBar">
      <img className="searchBar__icon" src={searchIcon} alt="Search icon" />

      <form className="searchBar__search" onSubmit={navigateToSearch}>
        <label htmlFor="search" className="visually-hidden">
          {pageInfo.placeholder}
        </label>
        <input
          className="searchBar__search--input"
          type="text"
          onChange={handleQuery}
          value={searchParams.query}
          id="search"
          name="search"
          placeholder={pageInfo.placeholder}
        />
      </form>
    </div>
  );
};

export default SearchBar;
