import { useAuth } from "@hooks/useAuth";
import Spinner from "@components/common/Spinner/Spinner";

import logo from "@assets/logo.svg";
import avatar from "@assets/image-avatar.png";

import Nav from "./Nav/Nav";
import "./Header.scss";

const Header = () => {
  const { logout, submission } = useAuth();

  return (
    <header className="header">
      <img className="header__logo" src={logo} alt="Netwix company logo" />
      <Nav />
      <div className="user-info">
        <img className="user-info__avatar" src={avatar} alt="User avatar" />
        <button className="user-info__button" type="submit" onClick={logout}>
          <Spinner
            loading={submission.status === "logging out"}
            variant="primary"
          />
          Logout
        </button>
      </div>
    </header>
  );
};

export default Header;
