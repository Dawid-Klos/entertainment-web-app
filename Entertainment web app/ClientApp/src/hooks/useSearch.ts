import { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";

import { pages } from "../config/constants";

export const useSearch = () => {
  const [query, setQuery] = useState("");
  const [pageInfo, setPageInfo] = useState(pages.home);

  const location = useLocation();
  const navigate = useNavigate();

  const navigateToSearch: React.FormEventHandler<HTMLFormElement> = (e) => {
    e.preventDefault();
    const userQuery = query.trim();

    if (!userQuery || userQuery.length < 3) {
      return;
    }

    const searchParams = new URLSearchParams(location.search);
    searchParams.set("query", userQuery);

    const searchPath =
      pageInfo.path === "/"
        ? "/search"
        : pageInfo.path.toLowerCase() + "/search";

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
    query,
    setQuery,
    navigateToSearch,
  };
};
