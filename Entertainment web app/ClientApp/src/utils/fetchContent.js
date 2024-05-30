import axios from "axios";

export const fetchBookmarked = async () => {
  try {
    const bookmarkedMovies = await axios.get("/api/user-content/movies");
    const bookmarkedTVSeries = await axios.get("/api/user-content/tv-series");

    return {
      movies: bookmarkedMovies.data,
      tvSeries: bookmarkedTVSeries.data,
    };
  } catch (error) {
    throw new Error(
      "Failed to download content, please check your internet connection or try later.",
    );
  }
};

export const fetchContent = async (path) => {
  try {
    const contentResponse = await axios.get(`/api/${path}`);

    return { data: contentResponse.data.data };
  } catch (error) {
    throw new Error(
      "Failed to download content, please check your internet connection or try later.",
    );
  }
};

export const searchMovies = async (query) => {
  try {
    const movies = await axios.get(`/api/movies/search?title=${query}`);

    return {
      result: movies.data.data,
      query: query ? query : "",
      category: "Movies",
    };
  } catch (error) {
    throw new Error("Failed to fetch search result, please try again later.");
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
    throw new Error("Failed to fetch search result, please try again later.");
  }
};

export const searchBookmarked = async (query) => {
  try {
    const [movies, tvSeries] = await Promise.all([
      axios.get(`/api/user-content/movies?title=${query}`),
      axios.get(`/api/user-content/tv-series?title=${query}`),
    ]);

    const searchResult = movies.data.concat(tvSeries.data);

    return {
      result: searchResult,
      query: query ? query : "",
      category: "Bookmarked",
    };
  } catch (error) {
    throw new Error("Failed to fetch search result, please try again later.");
  }
};

export const searchContent = async (query) => {
  try {
    const [movies, tvSeries] = await Promise.all([
      axios.get(`/api/movies/search?title=${query}`),
      axios.get(`/api/tv-series/search?title=${query}`),
    ]);
    const searchResult = movies.data.data.concat(tvSeries.data.data);

    return {
      result: searchResult,
      query: query ? query : "",
      category: "Library",
    };
  } catch (error) {
    throw new Error("Failed to fetch search result, please try again later.");
  }
};
