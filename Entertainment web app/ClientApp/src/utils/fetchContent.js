import axios from "axios";

import { pages } from '../config/constants';

const fixCategoryFormat = (path) => { 
    const category = Object.values(pages).find(page => page.path.includes(path));

    return category ? category.category : "";
};

export const fetchAllMovies = async () => {
    const response = await axios
        .get('/api/Movies/GetAllMovies')
        .catch(error => {
            throw new Error("Failed to fetch movies, please try again later.");
        });

    return response.data
}

export const fetchTvSeries = async () => {
    const response = await axios
        .get('/api/Movies/GetTvSeries')
        .catch(error => {
            throw new Error("Failed to fetch TV series, please try again later.");
        });

    return response.data
}

export const fetchTrendingMovies = async () => {
    const response = await axios
        .get('/api/Movies/GetTrendingMovies')
        .catch(error => {
            throw new Error("Failed to fetch trending movies, please try again later.");
        });

    return response.data
}

export const fetchHomeContent = async () => {
    return { recommended: await fetchAllMovies(), trending: await fetchTrendingMovies()};
}

export const fetchSearchResult = async (params) => {
    const category = fixCategoryFormat(params.category);
    
    let response;
    
    if(category === "Library" && params.query) {
        response = await axios
            .get(`/api/Search/SearchByTitle?title=${params.query}`)
            .catch(error => {
                throw new Error("Failed to fetch search result, please try again later.");
            });
        
        return { data: response.data, query: params.query, category: "Library" };
    }
    
    
    if(!params.query) {
        response = await axios
            .get(`/api/Search/SearchByCategory?category=${category}`)
            .catch(error => {
                throw new Error("Failed to fetch search result, please try again later.");
            });
        
        return { data: response.data, query: "", category: category };
    }
    
    if(params.query && category) {
        response = await axios
            .get(`/api/Search/SearchByCategoryAndTitle?category=${category}&title=${params.query}`)
            .catch(error => {
                throw new Error("Failed to fetch search result, please try again later.");
            });

        return { data: response.data, query: params.query, category: category };
    }
    
    return null;
}