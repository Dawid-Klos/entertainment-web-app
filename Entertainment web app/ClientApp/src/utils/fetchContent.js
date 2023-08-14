import axios from "axios";

export const fetchAllMovies = async () => {
    const response = await axios
        .get('/api/Movies/GetAllMovies')
        .catch(error => {
            console.log(error);
            return null;
        });

    return response.data
}

export const fetchTrendingMovies = async () => {
    const response = await axios
        .get('/api/Movies/GetTrendingMovies')
        .catch(error => {
            console.log(error);
            return null;
        });

    return response.data
}

export const fetchTvSeries = async () => {
    const response = await axios
        .get('/api/Movies/GetTvSeries')
        .catch(error => {
            console.log(error);
            return null;
        });

    return response.data
}

export const fetchMovie = async ({ params }) => {
    const response = await axios
        .get(`/api/Movies/GetMovie/${params}`)
        .catch(error => {
            console.log(error);
            return null;
        });

    return response.data
}

export const fetchHomeContent = async () => {
    return { recommended: await fetchAllMovies(), trending: await fetchTrendingMovies()};
}