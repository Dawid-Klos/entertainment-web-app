import {Navigate} from 'react-router-dom';
import Cookies from 'js-cookie';

import {fetchAllMovies, fetchHomeContent, fetchMovie, fetchTvSeries} from '../utils/fetchContent';

import Layout from '../components/Layout';
import Movies from '../components/Movies/Movies';
import Bookmark from '../components/Bookmark/Bookmark'
import TVSeries from '../components/TVSeries/TVSeries';
import Home from '../components/Home/Home';
import Search from '../components/Search/Search';
import Login from '../components/Auth/Login/Login';
import Register from '../components/Auth/Register/Register';

const PrivateRoute = ({children}) => {
    const authCookie = Cookies.get('_auth');
    
    if (!authCookie) {
        console.log("auth is null");
        return <Navigate to="/login"/>;
    } else {
        return children;
    }
};

const routerConfig = [
    {
        errorElement: <div>Not Found</div>,
        children: [
            {
                path: '/',
                element: <PrivateRoute> <Layout/> </PrivateRoute>,
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