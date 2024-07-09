import axios from "axios";
import type { ActionFunctionArgs } from "react-router-dom";

export const bookmarkAction = async ({ request }: ActionFunctionArgs) => {
  let formData = await request.formData();

  const movieId = formData.get("movieId");
  const isBookmarked = formData.get("isBookmarked") === "true";

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
