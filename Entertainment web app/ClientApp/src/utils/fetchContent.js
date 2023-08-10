import Cookies from "js-cookie";
import axios from "axios";

const getConfig = () => {
    const token = Cookies.get('_auth');

    return {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    };
}

export const fetchAllMovies = async () => {
    const response = await axios
        .get('/api/Movies/GetAllMovies', getConfig())
        .catch(error => {
            console.log(error);
            return null;
        });

    return response.data
}

export const fetchTrendingMovies = async () => {
    const response = await axios
        .get('/api/Movies/GetTrendingMovies', getConfig())
        .catch(error => {
            console.log(error);
            return null;
        });

    return response.data
}

export const fetchTvSeries = async () => {
    const response = await axios
        .get('/api/Movies/GetTvSeries', getConfig())
        .catch(error => {
            console.log(error);
            return null;
        });

    return response.data
}

export const fetchMovie = async ({ params }) => {
    const response = await axios
        .get(`/api/Movies/GetMovie/${params}`, getConfig())
        .catch(error => {
            console.log(error);
            return null;
        });

    return response.data
}

export const fetchHomeContent = async () => {
    return { recommended: await fetchAllMovies(), trending: await fetchTrendingMovies()};
}