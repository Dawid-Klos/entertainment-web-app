import { useState, useEffect, useCallback } from "react";

import logo from "@assets/logo.svg";
import avatar from "@assets/image-avatar.png";
import MoreIcon from "@assets/icons/MoreIcon";
import type { User } from "@commonTypes/auth.types";

import Nav from "./Nav/Nav";
import HamburgerMenu from "./HamburgerMenu/HamburgerMenu";

import "./Header.scss";

type HeaderProps = {
  userInfo: User;
};

const Header = ({ userInfo }: HeaderProps) => {
  const [headerState, setHeaderState] = useState("off");

  const toggleHeaderState = () => {
    setHeaderState((prevState) => (prevState === "on" ? "hiding" : "on"));
  };

  const handleClickOutside = useCallback((event: MouseEvent) => {
    const target = event.target as HTMLElement;

    if (!target.closest(".header")) {
      setHeaderState("hiding");
    }
  }, []);

  useEffect(() => {
    if (headerState === "hiding") {
      window.removeEventListener("click", handleClickOutside);

      setTimeout(() => {
        setHeaderState("off");
      }, 500);
    }

    if (headerState === "on") {
      window.addEventListener("click", handleClickOutside);
    }

    return () => {
      window.removeEventListener("click", handleClickOutside);
    };
  }, [headerState, handleClickOutside]);

  return (
    <header
      className={`header ${headerState === "on" ? "header--expanded" : headerState === "hiding" && "header--hidden"}`}
    >
      <img className="header__logo" src={logo} alt="Netwix company logo" />
      <Nav
        isExpanded={headerState === "on"}
        collapseHeader={toggleHeaderState}
      />
      <div className="user-info">
        <button
          className="user-info__button"
          type="submit"
          onClick={toggleHeaderState}
        >
          <img className="user-info__avatar" src={avatar} alt="User avatar" />
          <MoreIcon
            className={`user-info__icon ${headerState === "on" && "user-info__icon--active"}`}
          />
        </button>
      </div>
      <HamburgerMenu state={headerState} user={userInfo} />
    </header>
  );
};

export default Header;
