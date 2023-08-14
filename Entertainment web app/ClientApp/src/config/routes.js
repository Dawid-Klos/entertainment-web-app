import {isRouteErrorResponse, redirect, useRouteError} from "react-router-dom";

import {fetchAllMovies, fetchHomeContent, fetchMovie, fetchTvSeries} from '../utils/fetchContent';
import {AuthenticateUser} from '../utils/authUser';

import Layout from '../components/Layout';
import Movies from '../components/Movies/Movies';
import Bookmark from '../components/Bookmark/Bookmark'
import TVSeries from '../components/TVSeries/TVSeries';
import Home from '../components/Home/Home';
import Search from '../components/Search/Search';
import Login from '../components/Auth/Login/Login';
import Register from '../components/Auth/Register/Register';


function ErrorBoundary() {
    const error = useRouteError();

    if (isRouteErrorResponse(error)) {
        if (error.status === 404) {
            return <div>This page doesn't exist!</div>;
        }

        if (error.status === 401) {
            return <div>You aren't authorized to see this</div>;
        }

        if (error.status === 503) {
            return <div>Looks like our API is down</div>;
        }

        if (error.status === 418) {
            return <div>🫖</div>;
        }
    }

    return <div>Something went wrong</div>;
}


const routerConfig = [
    {
        errorElement: <ErrorBoundary/>,
        element: <Layout/>,
        loader: async () => {
            const userLoggedIn = await AuthenticateUser();

            if (userLoggedIn === null) {
                return redirect('/login');
            }

            return null;
        },
        children: [
            {
                path: '/',
                element: <Home/>,
                loader: () => fetchHomeContent(),
                children: [
                    {
                        path: ':query',
                        element: <Search/>
                    }
                ]
            },
            {
                path: '/movies',
                element: <Movies/>,
                loader: () => fetchAllMovies(),
                children: [
                    {
                        path: ':query',
                        loader: ({params}) => fetchMovie(params),
                        element: <Search/>
                    }
                ]
            },
            {
                path: '/tv-series',
                element: <TVSeries/>,
                loader: () => fetchTvSeries(),
                children: [
                    {
                        path: ':query',
                        element: <Search/>
                    }
                ]
            },
            {
                path: '/bookmarked',
                element: <Bookmark/>,
                errorElement: <ErrorBoundary/>,
                children: [
                    {
                        path: ':query',
                        element: <Search/>
                    }
                ],
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