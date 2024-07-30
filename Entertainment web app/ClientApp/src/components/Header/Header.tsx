import { useState } from "react";

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
  const [dropdownState, setDropdownState] = useState("off");

  const toggleDropdown = () => {
    if (dropdownState === "on") {
      setDropdownState("hiding");

      setTimeout(() => {
        setDropdownState("off");
      }, 500);
    }

    dropdownState === "off" && setDropdownState("on");
  };

  return (
    <header className="header">
      <img className="header__logo" src={logo} alt="Netwix company logo" />
      <Nav />
      <div className="user-info">
        <button
          className="user-info__button"
          type="submit"
          onClick={toggleDropdown}
        >
          <img className="user-info__avatar" src={avatar} alt="User avatar" />
          <MoreIcon
            className={`user-info__icon ${dropdownState === "on" && "user-info__icon--active"}`}
          />
        </button>
      </div>
      <HamburgerMenu state={dropdownState} user={userInfo} />
    </header>
  );
};

export default Header;
