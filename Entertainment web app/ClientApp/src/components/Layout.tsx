import { Outlet } from "react-router-dom";

import Header from "@components/Header/Header";
import SearchBar from "@components/SearchBar/SearchBar";

const Layout = () => {
  return (
    <>
      <Header />
      <SearchBar />
      <Outlet />
    </>
  );
};

export default Layout;
