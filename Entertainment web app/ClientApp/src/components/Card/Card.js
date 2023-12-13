import { useEffect, useState } from "react";
import axios from "axios";

import emptyBookmarkIcon from "../../assets/icon-bookmark-empty.svg";
import fullBookmarkIcon from "../../assets/icon-bookmark-full.svg";
import categoryMovieIcon from "../../assets/icon-category-movie.svg";
import categoryTvIcon from "../../assets/icon-category-tv.svg";

import "./Card.scss";

const Card = ({ movie, bookmarks }) => {
  const [cardInfo, setCardInfo] = useState({ id: 0, isBookmarked: false });
  const { MovieId, ImgSmall, Title, Category, Year, Rating } = movie;

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

  const handleAddBookmark = async () => {
    cardInfo.isBookmarked ? await removeBookmark() : await addBookmark();
    setCardInfo({ ...cardInfo, isBookmarked: !cardInfo.isBookmarked });
  };
  useEffect(() => {
    if (movie && bookmarks) {
      setCardInfo({ id: MovieId, isBookmarked: bookmarks.includes(MovieId) });
    }
  }, [movie, bookmarks, MovieId]);

  return (
    <div className="card">
      <div
        className="top"
        style={{
          background: "url(" + ImgSmall + ")",
          backgroundSize: "cover",
          backgroundPosition: "center",
          backgroundRepeat: "no-repeat",
        }}
      >
        <button
          onClick={handleAddBookmark}
          className="top__bookmark"
          aria-label="Adds a movie to bookmarks"
        >
          <img
            src={cardInfo.isBookmarked ? fullBookmarkIcon : emptyBookmarkIcon}
            alt="Adds a movie to bookmarks"
          />
        </button>
      </div>
      <div className="bottom">
        <div className="info">
          <p>{Year}</p>
          <span className="info__separator"></span>
          <p className="info__category">
            <img
              src={Category === "Movie" ? categoryMovieIcon : categoryTvIcon}
              alt={Category === "Movie" ? "Movie" : "TV Series"}
            />
            {Category}
          </p>
          <span className="info__separator"></span>
          <p>{Rating}</p>
        </div>
        <h2 className="bottom__title">{Title}</h2>
      </div>
    </div>
  );
};

export default Card;
