import axios from "axios";

export const fetchBookmarked = async () => {
  try {
    const bookmarksResponse = await axios.get("/api/Bookmark/GetBookmarks");

    return bookmarksResponse.data.map((bookmark) => bookmark.MovieId);
  } catch (error) {
    throw new Error(
      "Failed to download content, please check your internet connection or try later.",
    );
  }
};

export const fetchContent = async (path) => {
  try {
    const contentResponse = await axios.get(`/api/${path}`);
    console.log("contentResponse ->", contentResponse);

    if (path.includes("Trending")) {
      const tempFix = contentResponse.data.map((item) => {
        return {
          Title: item.Movie.Title,
          Year: item.Movie.Year,
          Category: item.Movie.Category,
          Rating: item.Movie.Rating,
          MovieId: item.Movie.MovieId,
          IsBookmarked: item.Movie.IsBookmarked,
          ImgSmall: item.ImgTrendingSmall,
          ImgLarge: item.ImgTrendingLarge,
        };
      });

      contentResponse.data = tempFix;
    }

    return { data: contentResponse.data };
  } catch (error) {
    throw new Error(
      "Failed to download content, please check your internet connection or try later.",
    );
  }
};

export const fetchSearchResult = async (query, category) => {
  const apiUrl = {
    byTitle: `/api/Search/SearchByTitle?title=${query}`,
    byCategoryAndTitle: `/api/Search/SearchByCategoryAndTitle?category=${category}&title=${query}`,
    byCategory: `/api/Search/SearchByCategory?category=${category}`,
  };

  try {
    let res;

    if (!category && !query) {
      const movies = await axios.get("/api/Movies/GetMovies");
      const tvSeries = await axios.get("/api/Movies/GetTvSeries");

      res = { data: [...movies.data, ...tvSeries.data] };
    } else if (!category || category === "") {
      res = await axios.get(apiUrl.byTitle);
    } else if (!query || query === null) {
      res = await axios.get(apiUrl.byCategory);
    } else if (category && query) {
      res = await axios.get(apiUrl.byCategoryAndTitle);
    } else {
      throw new Error("Invalid search query, please try again.");
    }

    const bookmarksResponse = await fetchBookmarked();

    return {
      data: res.data,
      bookmarks: bookmarksResponse,
      query: query ? query : "",
      category: category ? category : "Library",
    };
  } catch (error) {
    throw new Error("Failed to fetch search result, please try again later.");
  }
};
