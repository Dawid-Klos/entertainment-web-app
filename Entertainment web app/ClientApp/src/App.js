import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Movies from './components/Movies/Movies';
import Bookmark from './components/Bookmark/Bookmark'
import TVSeries from './components/TVSeries/TVSeries';
import Home from './components/Home/Home';
import Login from './components/Auth/Login/Login';
import Register from './components/Auth/Register/Register';
import Search from './components/Search/Search';
// import PageNotFound from './components/PageNotFound/PageNotFound';

import './custom.scss';


const App = () => (
    <Routes>
        <Route path="/" element={<Layout />}>
            <Route path="/">
                <Route index element={<Home />} />
                <Route path=":query" element={<Search />} />
            </Route>
            <Route path="/movies">
                <Route index element={<Movies />} />
                <Route path=":query" element={<Search />} />
            </Route>
            <Route path="/tv-series">
                <Route index element={<TVSeries />} />
                <Route path=":query" element={<Search />} />
            </Route>
            <Route path="/bookmarked">
                <Route index element={<Bookmark />} />
                <Route path=":query" element={<Search />} />
            </Route>
            {/* <Route path="*" element={<PageNotFound />} /> */}
        </Route>

        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
    </Routes>

);

export default App;