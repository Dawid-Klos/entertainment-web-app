import MenuHomeIcon from "@assets/icons/MenuHomeIcon";
import MenuMoviesIcon from "@assets/icons/MenuMoviesIcon";
import MenuTVSeriesIcon from "@assets/icons/MenuTVSeriesIcon";
import MenuBookmarkedIcon from "@assets/icons/MenuBookmarkedIcon";

import "./Link.scss";

type LinkProps = {
  isExpanded: boolean;
  isActive: boolean;
  to: string;
};

const Link = ({ isExpanded, isActive, to }: LinkProps) => {
  if (to === "home") {
    return (
      <div className={`link ${isActive ? "link--active" : ""}`}>
        <MenuHomeIcon className="link__icon" />
        <p className={`link__text ${isExpanded ? "link__text--visible" : ""}`}>
          Home
        </p>
      </div>
    );
  }

  if (to === "movies") {
    return (
      <div className={`link ${isActive ? "link--active" : ""}`}>
        <MenuMoviesIcon className="link__icon" />
        <p className={`link__text ${isExpanded ? "link__text--visible" : ""}`}>
          Movies
        </p>
      </div>
    );
  }

  if (to === "tv-series") {
    return (
      <div className={`link ${isActive ? "link--active" : ""}`}>
        <MenuTVSeriesIcon className="link__icon" />
        <p className={`link__text ${isExpanded ? "link__text--visible" : ""}`}>
          TV Series
        </p>
      </div>
    );
  }

  if (to === "bookmarked") {
    return (
      <div className={`link ${isActive ? "link--active" : ""}`}>
        <MenuBookmarkedIcon className="link__icon" />
        <p className={`link__text ${isExpanded ? "link__text--visible" : ""}`}>
          Bookmarked
        </p>
      </div>
    );
  }
};

export default Link;
