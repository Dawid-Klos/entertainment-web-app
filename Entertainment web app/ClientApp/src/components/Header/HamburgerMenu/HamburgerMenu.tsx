import { NavLink } from "react-router-dom";

import { useSignIn } from "@hooks/useSignIn";
import Spinner from "@components/common/Spinner/Spinner";

import "./HamburgerMenu.scss";

interface HamburgerMenuProps {
  state: "on" | "off" | "hiding";
  user: {
    firstname: string;
    lastname: string;
    email: string;
  };
}

const HamburgerMenu = ({ state, user }: HamburgerMenuProps) => {
  const { signOut, submission } = useSignIn();

  return (
    <div
      className={`dropdown ${state === "on" ? "dropdown--active" : state === "hiding" && "dropdown--hidden"}`}
    >
      <div className="dropdown-header">
        <p className="dropdown-header__title">
          {`${user.firstname} ${user.lastname}`}
        </p>
        <p className="dropdown-header__subtitle">{user.email}</p>
      </div>
      <span className="dropdown__line"></span>
      <NavLink className="dropdown__link" to="/profile">
        Profile
      </NavLink>
      <NavLink className="dropdown__link" to="/settings">
        Settings
      </NavLink>

      <span className="dropdown__line"></span>
      <button className="dropdown__logout" type="submit" onClick={signOut}>
        {submission.status === "signing out" ? (
          <Spinner loading={true} variant="primary" />
        ) : (
          "Logout"
        )}
      </button>
    </div>
  );
};

export default HamburgerMenu;
