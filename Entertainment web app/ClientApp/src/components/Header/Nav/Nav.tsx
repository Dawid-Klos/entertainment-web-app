import { NavLink, useLocation } from "react-router-dom";

import Link from "./Link/Link";

import "./Nav.scss";

type NavProps = {
  isExpanded: boolean;
  collapseHeader: () => void;
};

const Nav = ({ isExpanded, collapseHeader }: NavProps) => {
  const { pathname } = useLocation();
  const isSearchPage = "/search".includes(pathname);

  const collpaseHeader = () => {
    if (!isExpanded) return;

    collapseHeader();
  };

  return (
    <nav className={`nav ${isExpanded ? "nav--expanded" : ""}`}>
      <NavLink className="nav__link" to="/" onClick={collpaseHeader}>
        {({ isActive }) => (
          <Link
            isActive={isActive || isSearchPage}
            to="home"
            isExpanded={isExpanded}
          />
        )}
      </NavLink>

      <NavLink className="nav__link" to="/movies" onClick={collpaseHeader}>
        {({ isActive }) => (
          <Link isActive={isActive} to="movies" isExpanded={isExpanded} />
        )}
      </NavLink>

      <NavLink className="nav__link" to="/tv-series" onClick={collpaseHeader}>
        {({ isActive }) => (
          <Link isActive={isActive} to="tv-series" isExpanded={isExpanded} />
        )}
      </NavLink>

      <NavLink className="nav__link" to="/bookmarked" onClick={collpaseHeader}>
        {({ isActive }) => (
          <Link isActive={isActive} to="bookmarked" isExpanded={isExpanded} />
        )}
      </NavLink>
    </nav>
  );
};

export default Nav;
