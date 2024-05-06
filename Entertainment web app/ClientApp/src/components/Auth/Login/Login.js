import { useRef } from "react";
import { Link } from "react-router-dom";

import { useAuth } from "../../../hooks/useAuth";
import Spinner from "../../common/Spinner/Spinner";

import logo from "../../../assets/logo.svg";
import "../Auth.scss";

const Login = () => {
  const email = useRef();
  const password = useRef();

  const { submission, handleSubmit } = useAuth();

  const submitForm = async (e) => {
    const body = {
      Email: email.current.value,
      Password: password.current.value,
    };

    await handleSubmit(e, body, "/api/auth/login");
  };

  return (
    <section className="auth-section">
      <img
        className="auth-section__logo"
        src={logo}
        alt="Netwix company logo"
      />

      <div className="auth-section__container">
        <form className="form" onSubmit={submitForm}>
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

          {(submission.status === "success" ||
            submission.status === "error") && (
            <p className="form__status">{submission.message}</p>
          )}

          <button className="form__submit-btn" type="submit">
            {submission.status === "submitting" ? "" : "Login to your account"}
            <Spinner loading={submission.status === "submitting"} />
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
