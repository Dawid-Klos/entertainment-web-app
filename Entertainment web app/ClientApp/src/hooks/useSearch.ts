import { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";

import { pages } from "../config/constants";

export const useSearch = () => {
  const [pageInfo, setPageInfo] = useState(pages.home);

  const location = useLocation();
  const navigate = useNavigate();

  const navigateToSearch = (e: any) => {
    e.preventDefault();

    const query = e.target.search.value;

    const searchParams = new URLSearchParams(location.search);
    searchParams.set("query", query);

    const searchPath =
      pageInfo.path === "/"
        ? "/search"
        : pageInfo.path.toLowerCase() + "/search";
    console.log("search path ", searchPath);
    navigate(`${searchPath}?${searchParams.toString()}`);
  };

  useEffect(() => {
    if (!location.pathname.includes("/search")) {
      let currentPage = Object.values(pages).find((page) =>
        location.pathname.includes(page.path),
      );

      currentPage = currentPage ? currentPage : pages.home;
      setPageInfo(currentPage);
    }
  }, [location.pathname]);

  return {
    pageInfo,
    navigateToSearch,
  };
};
