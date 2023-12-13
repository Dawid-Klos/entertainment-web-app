import axios from "axios";

import { pages } from "../config/constants";

const fixCategoryFormat = (path) => {
  const category = Object.values(pages).find((page) =>
    page.path.includes(path),
  );

  return category ? category.category : "";
};

export const fetchBookmarked = async () => {
  let content;

  try {
    const bookmarksResponse = await axios.get("/api/Bookmark/GetBookmarks");
    content = bookmarksResponse.data.map((bookmark) => bookmark.MovieId);
  } catch (error) {
    throw new Error(
      "Failed to download content, please check your internet connection or try later.",
    );
  }

  return content;
};

export const fetchContent = async (path) => {
  const content = {
    data: [],
    bookmarks: [],
  };

  try {
    const contentResponse = await axios.get(`/api/${path}`);
    content.data = contentResponse.data;

    content.bookmarks = await fetchBookmarked();
  } catch (error) {
    throw new Error(
      "Failed to download content, please check your internet connection or try later.",
    );
  }

  return content;
};

export const fetchSearchResult = async (params) => {
  const category = fixCategoryFormat(params.category);

  let response;

  if (category === "Library" && params.query) {
    response = await axios
      .get(`/api/Search/SearchByTitle?title=${params.query}`)
      .catch((error) => {
        throw new Error(
          "Failed to fetch search result, please try again later.",
        );
      });

    return { data: response.data, query: params.query, category: "Library" };
  }

  if (!params.query) {
    response = await axios
      .get(`/api/Search/SearchByCategory?category=${category}`)
      .catch((error) => {
        throw new Error(
          "Failed to fetch search result, please try again later.",
        );
      });

    return { data: response.data, query: "", category: category };
  }

  if (params.query && category) {
    response = await axios
      .get(
        `/api/Search/SearchByCategoryAndTitle?category=${category}&title=${params.query}`,
      )
      .catch((error) => {
        throw new Error(
          "Failed to fetch search result, please try again later.",
        );
      });

    return { data: response.data, query: params.query, category: category };
  }

  return null;
};
