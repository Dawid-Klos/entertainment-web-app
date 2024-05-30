import { redirect } from "react-router-dom";
import {
  fetchContent,
  fetchBookmarked,
  searchMovies,
  searchTVSeries,
  searchBookmarked,
  searchContent,
} from "../utils/fetchContent";
import { AuthenticateUser } from "../utils/authUser";
import { bookmarkAction } from "../utils/actions";

import Layout from "../components/Layout";
import Movies from "../components/Movies/Movies";
import Bookmark from "../components/Bookmark/Bookmark";
import TVSeries from "../components/TVSeries/TVSeries";
import Home from "../components/Home/Home";
import Search from "../components/Search/Search";
import Login from "../components/Auth/Login/Login";
import Register from "../components/Auth/Register/Register";
import ErrorBoundary from "../components/common/ErrorBoundary/ErrorBoundary";

const routerConfig = [
  {
    errorElement: <ErrorBoundary />,
    element: <Layout />,
    loader: async () => {
      const userLoggedIn = await AuthenticateUser();

      if (userLoggedIn === null || userLoggedIn === false) {
        return redirect("/login");
      }

      return null;
    },
    children: [
      {
        path: "/",
        element: <Home />,
        loader: async () => {
          const recommended = await fetchContent("movies");
          const trending = await fetchContent("trending");

          return { recommended: recommended, trending: trending };
        },
        action: bookmarkAction,
      },
      {
        path: "/search",
        element: <Search />,
        loader: ({ request }) => {
          const url = new URL(request.url);
          const query = url.searchParams.get("query");
          return searchContent(query);
        },
        action: bookmarkAction,
      },
      {
        path: "/movies",
        element: <Movies />,
        loader: () => fetchContent("movies"),
        action: bookmarkAction,
      },
      {
        path: "/movies/search",
        element: <Search />,
        loader: ({ request }) => {
          const url = new URL(request.url);
          const query = url.searchParams.get("query");
          return searchMovies(query);
        },
        action: bookmarkAction,
      },
      {
        path: "/tv-series",
        element: <TVSeries />,
        loader: () => fetchContent("tv-series"),
        action: bookmarkAction,
      },
      {
        path: "/tv-series/search",
        element: <Search />,
        loader: ({ request }) => {
          const url = new URL(request.url);
          const query = url.searchParams.get("query");
          return searchTVSeries(query);
        },
        action: bookmarkAction,
      },
      {
        path: "/bookmarked",
        element: <Bookmark />,
        loader: () => fetchBookmarked(),
        action: bookmarkAction,
      },
      {
        path: "/bookmarked/search",
        element: <Search />,
        loader: ({ request }) => {
          const url = new URL(request.url);
          const query = url.searchParams.get("query");
          return searchBookmarked(query);
        },
        action: bookmarkAction,
      },
    ],
  },
  {
    errorElement: <ErrorBoundary />,
    path: "/login",
    element: <Login />,
  },
  {
    errorElement: <ErrorBoundary />,
    path: "/register",
    element: <Register />,
  },
];

export default routerConfig;
