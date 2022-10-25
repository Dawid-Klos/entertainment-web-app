import "./Nav.scss";
import {NavLink} from "react-router-dom";

const Nav = () => {

    return(
        <nav className="nav">
            <a className="nav__title">Test of SCSS</a>
            <NavLink to="/trending">Trending</NavLink>
        </nav>
    )
}

export default Nav;