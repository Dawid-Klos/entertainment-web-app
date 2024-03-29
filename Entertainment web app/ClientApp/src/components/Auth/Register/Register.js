import { Link } from "react-router-dom";
import { useRef } from "react";

import { useAuth } from "../../../hooks/useAuth";
import logo from "../../../assets/logo.svg";
import "../Auth.scss";
import Spinner from "../../common/Spinner/Spinner";

const Register = () => {
  const firstName = useRef();
  const lastName = useRef();
  const email = useRef();
  const password = useRef();
  const confirmPassword = useRef();

  const { submission, handleSubmit } = useAuth();

  const submitForm = async (e) => {
    e.preventDefault();

    const body = {
      Email: email.current.value,
      Firstname: firstName.current.value,
      Lastname: lastName.current.value,
      Password: password.current.value,
      ConfirmPassword: confirmPassword.current.value,
    };

    await handleSubmit(e, body, "/api/Auth/Register");
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
          <h2 className="form__title">Sign up</h2>
          <div className="input-container">
            <label htmlFor="email" className="input-container__label">
              Email address
            </label>
            <input
              className="input-container__input"
              type="text"
              ref={email}
              name="email"
              id="email"
            />
          </div>
          <div className="input-container">
            <label htmlFor="firstname" className="input-container__label">
              Firstname
            </label>
            <input
              className="input-container__input"
              type="text"
              ref={firstName}
              name="firstname"
              id="firstname"
            />
          </div>
          <div className="input-container">
            <label htmlFor="firstname" className="input-container__label">
              Lastname
            </label>
            <input
              className="input-container__input"
              type="text"
              ref={lastName}
              name="Lastname"
              id="Lastname"
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
              name="password"
              id="password"
            />
          </div>
          <div className="input-container">
            <label
              htmlFor="confirm-password"
              className="input-container__label"
            >
              Confirm Password
            </label>
            <input
              className="input-container__input"
              type="password"
              ref={confirmPassword}
              id="confirm-password"
            />
          </div>

          {(submission.status === "success" ||
            submission.status === "error") && (
            <p className="form__status">{submission.message}</p>
          )}

          {submission.errors && submission.errors.length > 1 && (
            <ul className="form__errors">
              {submission.errors.map((error, index) => (
                <li key={index}>{error}</li>
              ))}
            </ul>
          )}

          <button className="form__submit-btn" type="submit">
            {submission.status === "submitting" ? "" : "Create an account"}
            <Spinner loading={submission.status === "submitting"} />
          </button>
        </form>

        <div className="create-account">
          <p className="create-account__text">Already have an account?</p>
          <Link className="create-account__link" to="/login">
            Login
          </Link>
        </div>
      </div>
    </section>
  );
};

export default Register;
