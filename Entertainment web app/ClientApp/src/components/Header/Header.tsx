import { useSignIn } from "@hooks/useSignIn";
import Spinner from "@components/common/Spinner/Spinner";

import logo from "@assets/logo.svg";
import avatar from "@assets/image-avatar.png";

import Nav from "./Nav/Nav";
import "./Header.scss";

const Header = () => {
  const { signOut, submission } = useSignIn();

  return (
    <header className="header">
      <img className="header__logo" src={logo} alt="Netwix company logo" />
      <Nav />
      <div className="user-info">
        <img className="user-info__avatar" src={avatar} alt="User avatar" />
        <button className="user-info__button" type="submit" onClick={signOut}>
          {submission.status === "signing out" ? (
            <Spinner loading={true} variant="primary" />
          ) : (
            "Logout"
          )}
        </button>
      </div>
    </header>
  );
};

export default Header;
