import axios from "axios";

export const bookmarkAction = async ({ request }) => {
  let formData = await request.formData();

  const movieId = formData.get("movieId");
  const isBookmarked = formData.get("isBookmarked") === "true";

  console.log(movieId, isBookmarked);

  try {
    if (isBookmarked) {
      await axios.delete("/api/users/bookmarks/" + movieId);
      return { movieId: movieId, isBookmarked: false };
    }

    if (!isBookmarked) {
      await axios.post("/api/users/bookmarks/" + movieId);
      return { movieId: movieId, isBookmarked: true };
    }

    return null;
  } catch (error) {
    throw new Error("Failed to handle bookmark");
  }
};
