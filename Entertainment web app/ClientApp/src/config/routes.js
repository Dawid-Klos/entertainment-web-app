import {redirect} from "react-router-dom";

import {fetchAllMovies, fetchHomeContent, fetchSearchResult, fetchTvSeries} from '../utils/fetchContent';
import {AuthenticateUser} from '../utils/authUser';

import Layout from '../components/Layout';
import Movies from '../components/Movies/Movies';
import Bookmark from '../components/Bookmark/Bookmark'
import TVSeries from '../components/TVSeries/TVSeries';
import Home from '../components/Home/Home';
import Search from '../components/Search/Search';
import Login from '../components/Auth/Login/Login';
import Register from '../components/Auth/Register/Register';
import ErrorBoundary from '../components/ErrorBoundary/ErrorBoundary';


const routerConfig = [
    {
        errorElement: <ErrorBoundary/>,
        element: <Layout/>,
        loader: async () => {
            const userLoggedIn = await AuthenticateUser();

            if (userLoggedIn === null || userLoggedIn === false) {
                return redirect('/login');
            }

            return null;
        },
        children: [
            {
                path: '/',
                exact: true,
                loader: () => redirect('/Library'),
            },
            {
                path: '/Library',
                exact: true,
                element: <Home/>,
                loader: () => fetchHomeContent(),
            },
            {
                path: '/Search/:category/:query?',
                element: <Search/>,
                loader: ({ params }) => fetchSearchResult(params),
            },
            {
                path: '/Movies',
                element: <Movies/>,
                loader: () => fetchAllMovies(),
            },
            {
                path: '/TV-series',
                element: <TVSeries/>,
                loader: () => fetchTvSeries(),
            },
            {
                path: '/Bookmarked',
                element: <Bookmark/>,
                errorElement: <ErrorBoundary/>,
            }
        ]
    },
    {
        errorElement: <ErrorBoundary/>,
        path: '/login',
        element: <Login/>
    },
    {
        errorElement: <ErrorBoundary/>,
        path: '/register',
        element: <Register/>
    }
];

export default routerConfig;