import Nav from "./Nav/Nav";
import SearchBar from "./SearchBar/SearchBar";
import {Outlet} from "react-router-dom";

const Layout = () => {
    return (
        <>
            <Nav />
            <SearchBar />
            <Outlet />
        </>
    )
}

export default Layout;