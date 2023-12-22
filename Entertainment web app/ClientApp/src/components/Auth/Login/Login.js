import { useRef } from "react";
import { Link, useNavigate } from "react-router-dom";

import logo from "../../../assets/logo.svg";
import "../Auth.scss";

const Login = () => {
  const email = useRef();
  const password = useRef();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    let res;

    const body = {
      Email: email.current.value,
      Password: password.current.value,
    };

    try {
      let login = await fetch("/api/Auth/Login", {
        method: "POST",
        credentials: "include",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      });

      res = await login.json();
    } catch (error) {
      console.log("Error: ", error);
    }

    if (res && res.isSuccess) {
      navigate("/Library");
    } else {
      console.log("There is some problem, user not logged in!");
      // TODO: Show error message to the user
    }
  };

  return (
    <section className="auth-section">
      <img
        className="auth-section__logo"
        src={logo}
        alt="Netwix company logo"
      />

      <div className="auth-section__container">
        <form className="form" onSubmit={handleSubmit}>
          <h2 className="form__title">Login</h2>

          <div className="form__inputs-wrapper">
            <div className="input-container">
              <label htmlFor="email" className="input-container__label">
                Email address
              </label>
              <input
                className="input-container__input"
                type="text"
                ref={email}
                id="email"
              />
            </div>
            <div className="input-container">
              <label htmlFor="password" className="input-container__label">
                Password
              </label>
              <input
                className="input-container__input"
                type="password"
                ref={password}
                id="password"
              />
            </div>
          </div>

          <button className="form__submit-btn" type="submit">
            Login to your account
          </button>
        </form>
        <div className="create-account">
          <p className="create-account__text">Don't have an account?</p>
          <Link className="create-account__link" to="/register">
            Sign Up
          </Link>
        </div>
      </div>
    </section>
  );
};

export default Login;
