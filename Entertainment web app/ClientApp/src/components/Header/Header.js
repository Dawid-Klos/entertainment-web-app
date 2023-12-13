import Nav from "./Nav/Nav";

import logo from "../../assets/logo.svg";
import avatar from "../../assets/image-avatar.png";

import "./Header.scss";

const Header = () => {
  return (
    <header className="header">
      <img className="header__logo" src={logo} alt="Netwix company logo" />
      <Nav />
      <img className="header__avatar" src={avatar} alt="User avatar" />
    </header>
  );
};

export default Header;
