import axios from "axios";

const validateResponse = (response) => {
  if (response.data.status === "error" || response.data.statusCode !== 200) {
    throw new Error(response.statusCode);
  }
};

export const fetchBookmarked = async () => {
  try {
    const bookmarkedMovies = await axios.get(`api/users/movies`);
    const bookmarkedTVSeries = await axios.get(`api/users/tv-series`);

    validateResponse(bookmarkedMovies);
    validateResponse(bookmarkedTVSeries);

    return {
      movies: bookmarkedMovies.data,
      tvSeries: bookmarkedTVSeries.data,
    };
  } catch (error) {
    throw new Error(500);
  }
};

export const fetchContent = async (path) => {
  try {
    const contentResponse = await axios.get(`/api/${path}`);

    validateResponse(contentResponse);

    return { data: contentResponse.data.data };
  } catch (error) {
    throw new Error(500);
  }
};

export const searchMovies = async (query) => {
  try {
    const movies = await axios.get(`/api/movies/search?title=${query}`);

    validateResponse(movies);

    return {
      result: movies.data.data,
      query: query ? query : "",
      category: "Movies",
    };
  } catch (error) {
    throw new Error(500);
  }
};

export const searchTVSeries = async (query) => {
  try {
    const tvSeries = await axios.get(`/api/tv-series/search?title=${query}`);

    return {
      result: tvSeries.data.data,
      query: query ? query : "",
      category: "TV Series",
    };
  } catch (error) {
    console.error(error);
    throw new Error(500);
  }
};

export const searchBookmarked = async (query) => {
  try {
    const [movies, tvSeries] = await Promise.all([
      axios.get(`/api/user-content/movies/search?title=${query}`),
      axios.get(`/api/user-content/tv-series/search?title=${query}`),
    ]);

    validateResponse(movies);
    validateResponse(tvSeries);

    const searchResult = movies.data.data.concat(tvSeries.data.data);

    return {
      result: searchResult,
      query: query ? query : "",
      category: "Bookmarked",
    };
  } catch (error) {
    throw new Error(500);
  }
};

export const searchContent = async (query) => {
  try {
    const [movies, tvSeries] = await Promise.all([
      axios.get(`/api/movies/search?title=${query}`),
      axios.get(`/api/tv-series/search?title=${query}`),
    ]);

    validateResponse(movies);
    const searchResult = movies.data.data.concat(tvSeries.data.data);

    return {
      result: searchResult,
      query: query ? query : "",
      category: "Library",
    };
  } catch (error) {
    throw new Error(500);
  }
};
