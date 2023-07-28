import {Navigate} from 'react-router-dom';
import {useIsAuthenticated} from "react-auth-kit";

import Layout from '../components/Layout';
import Movies from '../components/Movies/Movies';
import Bookmark from '../components/Bookmark/Bookmark'
import TVSeries from '../components/TVSeries/TVSeries';
import Home from '../components/Home/Home';
import Search from '../components/Search/Search';
import Login from '../components/Auth/Login/Login';
import Register from '../components/Auth/Register/Register';

const PrivateRoute = ({children}) => {
    const isAuthenticated = useIsAuthenticated();
    return isAuthenticated() ? children : <Navigate to="/login"/>;
};

const routerConfig = [
    {
        errorElement: <div>Not Found</div>,
        children: [
            {
                element: <PrivateRoute/>,
                children: [
                    {
                        path: '/',
                        element: <Layout/>,
                        children: [
                            {
                                path: '/',
                                element: <Home/>,
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
                                children: [
                                    {
                                        path: ':query',
                                        element: <Search/>
                                    }
                                ]
                            },
                            {
                                path: '/tv-series',
                                element: <TVSeries/>,
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
                                children: [
                                    {
                                        path: ':query',
                                        element: <Search/>
                                    }
                                ],
                            }
                        ]
                    }
                ]
            },
            {
                path: '/login',
                element: <Login/>
            },
            {
                path: '/register',
                element: <Register/>
            }
        ]
    }
];

export default routerConfig;