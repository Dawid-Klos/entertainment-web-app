import {Route, Routes} from 'react-router-dom';
import './custom.scss';
import Layout from './components/Layout';
import Movies from './components/Movies/Movies';
import Bookmark from './components/Bookmark/Bookmark'
import TVSeries from './components/TVSeries/TVSeries';
import Home from './components/Home/Home';
import Login from './components/Auth/Login/Login';
import Register from './components/Auth/Register/Register';

const App = () => {

    return (
        <>
            <Routes>
                <Route path="/" element={<Layout/>}>
                    <Route index element={<Home />}/>
                    <Route path="/movies" element={<Movies /> }/>
                    <Route path="/tv-series" element={<TVSeries />}/>
                    <Route path="/bookmarked" element={<Bookmark />}/>
                </Route>
                <Route path="/login" element={<Login />}/>
                <Route path="/register" element={<Register />}/>
            </Routes>
        </>
    )
}
  
export default App;