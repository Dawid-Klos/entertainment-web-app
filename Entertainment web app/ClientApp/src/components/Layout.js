import {Outlet} from "react-router-dom";

import Header from "./Header/Header";
import SearchBar from './SearchBar/SearchBar';

const Layout = () => {
    
    return (
        <>
            <Header />
            <SearchBar />
            <Outlet />
        </>
    )
}

export default Layout;