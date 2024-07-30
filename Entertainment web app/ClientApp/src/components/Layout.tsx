import { Outlet, useLoaderData } from "react-router-dom";

import Header from "@components/Header/Header";
import SearchBar from "@components/SearchBar/SearchBar";
import type { User } from "@commonTypes/auth.types";

type UserData = {
  userData: User;
};

const Layout = () => {
  const userData = useLoaderData() as UserData;

  return (
    <>
      <Header userInfo={userData} />
      <SearchBar />
      <Outlet />
    </>
  );
};

export default Layout;
