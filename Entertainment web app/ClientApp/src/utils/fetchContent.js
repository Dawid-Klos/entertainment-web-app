import axios from "axios";

import { pages } from "../config/constants";

const fixCategoryFormat = (path) => {
  const category = Object.values(pages).find((page) =>
    page.path.includes(path),
  );

  return category ? category.category : "";
};

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
    const bookmarksResponse = await fetchBookmarked();

    return { data: contentResponse.data, bookmarks: bookmarksResponse };
  } catch (error) {
    throw new Error(
      "Failed to download content, please check your internet connection or try later.",
    );
  }
};

export const fetchSearchResult = async (params) => {
  const category = fixCategoryFormat(params.category);

  if (category === "Library" && params.query) {
    try {
      const response = await axios.get(
        `/api/Search/SearchByTitle?title=${params.query}`,
      );
      const bookmarksResponse = await fetchBookmarked();

      return {
        data: response.data,
        bookmarks: bookmarksResponse,
        query: params.query,
        category: "Library",
      };
    } catch (error) {
      throw new Error("Failed to fetch search result, please try again later.");
    }
  }

  if (!params.query) {
    try {
      const response = await axios.get(
        `/api/Search/SearchByCategory?category=${category}`,
      );
      const bookmarksResponse = await fetchBookmarked();

      return {
        data: response.data,
        bookmarks: bookmarksResponse,
        query: params.query,
        category: category,
      };
    } catch (error) {
      throw new Error("Failed to fetch search result, please try again later.");
    }
  }

  if (params.query && category) {
    try {
      const response = await axios.get(
        `/api/Search/SearchByCategoryAndTitle?category=${category}&title=${params.query}`,
      );
      const bookmarksResponse = await fetchBookmarked();

      return {
        data: response.data,
        bookmarks: bookmarksResponse,
        query: params.query,
        category: category,
      };
    } catch (error) {
      throw new Error("Failed to fetch search result, please try again later.");
    }
  }

  throw new Error("Failed to fetch search result, please try again later.");
};
