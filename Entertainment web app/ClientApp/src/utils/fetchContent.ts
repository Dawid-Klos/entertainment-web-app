import axios from "axios";

export const fetchBookmarked = async () => {
  try {
    const bookmarkedMovies = axios.get(`api/users/movies`);
    const bookmarkedTVSeries = axios.get(`api/users/tv-series`);

    const bookmarkedContent = await Promise.all([
      bookmarkedMovies,
      bookmarkedTVSeries,
    ]);

    return {
      movies: bookmarkedContent[0].data || [],
      tvSeries: bookmarkedContent[1].data || [],
    };
  } catch (error) {
    throw new Error("Failed to fetch bookmarked content");
  }
};

export const fetchContent = async (path: string) => {
  try {
    const contentResponse = await axios.get(`/api/${path}`);

    return { data: contentResponse.data.data || [] };
  } catch (error) {
    throw new Error("Failed to fetch content, please try again later");
  }
};

export const searchMovies = async (query: string) => {
  try {
    const movies = await axios.get(`/api/movies/search?title=${query}`);

    return {
      result: movies.data.data || [],
      query: query ? query : "",
      category: "Movies",
    };
  } catch (error) {
    throw new Error("Failed to search movies, please try again later");
  }
};

export const searchTVSeries = async (query: string) => {
  try {
    const tvSeries = await axios.get(`/api/tv-series/search?title=${query}`);

    return {
      result: tvSeries.data.data || [],
      query: query ? query : "",
      category: "TV Series",
    };
  } catch (error) {
    throw new Error("Failed to search TV series, please try again later");
  }
};

export const searchBookmarked = async (query: string) => {
  try {
    const [movies, tvSeries] = await Promise.all([
      axios.get(`/api/users/movies/search?title=${query}`),
      axios.get(`/api/users/tv-series/search?title=${query}`),
    ]);

    if (!movies.data.data && !tvSeries.data.data) {
      return { result: [], query: query ? query : "", category: "Bookmarked" };
    }
    
    const searchResult = movies.data.data.concat(tvSeries.data.data);

    return {
      result: searchResult,
      query: query ? query : "",
      category: "Bookmarked",
    };
  } catch (error) {
    throw new Error(
      "Failed to search bookmarked content, please try again later",
    );
  }
};

export const searchContent = async (query: string) => {
  try {
    const [movies, tvSeries] = await Promise.all([
      axios.get(`/api/movies/search?title=${query}`),
      axios.get(`/api/tv-series/search?title=${query}`),
    ]);
    
    if (!movies.data.data && !tvSeries.data.data) {
      return { result: [], query: query ? query : "", category: "Library" };
    }
    const searchResult = movies.data.data.concat(tvSeries.data.data);

    return {
      result: searchResult,
      query: query ? query : "",
      category: "Library",
    };
  } catch (error) {
    throw new Error("Failed to search content, please try again later");
  }
};
