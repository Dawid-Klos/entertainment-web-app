import axios from "axios";
import { useState } from "react";

export default function useBookmark() {
  const [cardInfo, setCardInfo] = useState({
    id: 0,
    title: "",
    year: "",
    category: "",
    rating: "",
    imgSmall: "",
    imgLarge: "",
    isBookmarked: false,
  });

  const removeBookmark = async () => {
    try {
      const res = await axios.delete(
        "/api/Bookmark/Remove?movieId=" + cardInfo.id,
      );
    } catch (error) {
      throw new Error("Failed to fetch search result, please try again later.");
    }
  };

  const addBookmark = async () => {
    try {
      const res = await axios.post("/api/Bookmark/Add?movieId=" + cardInfo.id);
    } catch (error) {
      throw new Error("Failed to fetch search result, please try again later.");
    }
  };

  const handleBookmark = async () => {
    cardInfo.isBookmarked ? await removeBookmark() : await addBookmark();
    setCardInfo({ ...cardInfo, isBookmarked: !cardInfo.isBookmarked });
  };

  return { cardInfo, setCardInfo, handleBookmark };
}
