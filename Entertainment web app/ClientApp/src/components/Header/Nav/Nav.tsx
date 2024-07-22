import { NavLink, useLocation } from "react-router-dom";

import MenuHomeIcon from "@assets/icons/MenuHomeIcon";
import MenuMoviesIcon from "@assets/icons/MenuMoviesIcon";
import MenuTVSeriesIcon from "@assets/icons/MenuTVSeriesIcon";
import MenuBookmarkedIcon from "@assets/icons/MenuBookmarkedIcon";

import "./Nav.scss";

const Nav = () => {
  const { pathname } = useLocation();
  const isSearchPage = "/search".includes(pathname);

  return (
    <nav className="nav">
      <NavLink className="nav__link" to="/">
        {({ isActive }) => (
          <MenuHomeIcon
            className={
              isSearchPage || isActive
                ? "nav__link--active"
                : "nav__link--default"
            }
          />
        )}
      </NavLink>
      <NavLink className="nav__link" to="/movies">
        {({ isActive }) => (
          <MenuMoviesIcon
            className={isActive ? "nav__link--active" : "nav__link--default"}
          />
        )}
      </NavLink>
      <NavLink className="nav__link" to="/tv-series">
        {({ isActive }) => (
          <MenuTVSeriesIcon
            className={isActive ? "nav__link--active" : "nav__link--default"}
          />
        )}
      </NavLink>
      <NavLink className="nav__link" to="/bookmarked">
        {({ isActive }) => (
          <MenuBookmarkedIcon
            className={isActive ? "nav__link--active" : "nav__link--default"}
          />
        )}
      </NavLink>
    </nav>
  );
};

export default Nav;
