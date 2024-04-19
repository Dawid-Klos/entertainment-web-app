import axios from "axios";

export const bookmarkAction = async ({ request }) => {
  let formData = await request.formData();

  const movieId = formData.get("movieId");
  const isBookmarked = formData.get("isBookmarked") === "true";

  try {
    if (isBookmarked) {
      await axios.delete("/api/Bookmark/Remove?movieId=" + movieId);
      return { movieId: movieId, isBookmarked: false };
    }

    if (!isBookmarked) {
      await axios.post("/api/Bookmark/Add?movieId=" + movieId);
      return { movieId: movieId, isBookmarked: true };
    }

    return null;
  } catch (error) {
    throw new Error("Failed to handle bookmark");
  }
};
