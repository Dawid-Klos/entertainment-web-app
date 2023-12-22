import { Link, useNavigate } from "react-router-dom";
import { useEffect, useRef, useState } from "react";

import logo from "../../../assets/logo.svg";
import "../Auth.scss";

const Register = () => {
  const [isSuccess, setIsSuccess] = useState(false);
  const [isSubmitted, setIsSubmitted] = useState(false);
  const [message, setMessage] = useState("");

  const navigate = useNavigate();
  const firstName = useRef();
  const lastName = useRef();
  const email = useRef();
  const password = useRef();
  const confirmPassword = useRef();

  const getDetails = () => {
    return {
      Email: email.current.value,
      Firstname: firstName.current.value,
      Lastname: lastName.current.value,
      Password: password.current.value,
      ConfirmPassword: confirmPassword.current.value,
    };
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const body = getDetails();

    try {
      let register = await fetch("/api/Auth/Register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
      });
      register = register.json();

      if (register && register.isSuccess) {
        console.log(register);

        setMessage(register.message);
        setIsSuccess(true);
        setIsSubmitted(true);
      }

      if (register && !register.isSuccess) {
        // unpack register.Errors object to a variable errors as one string
        let errors = Object.values(register.errors).join(" ");
        console.log("errors: ", errors);

        setMessage(errors);
        setIsSuccess(false);
        setIsSubmitted(true);
      }
    } catch (error) {
      setMessage(error);
      setIsSuccess(false);
      setIsSubmitted(true);

      console.log(error);
    }
  };

  useEffect(() => {
    if (isSuccess && isSubmitted) {
      setTimeout(() => {
        navigate("/Library");
      }, 3000);
    }
  }, [isSubmitted]);

  return (
    <section className="auth-section">
      <img
        className="auth-section__logo"
        src={logo}
        alt="Netwix company logo"
      />

      <div className="auth-section__container">
        <form className="form" onSubmit={handleSubmit}>
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
          {isSuccess && isSubmitted && (
            <p className="form__success">
              Account created successfully!
              <br />
              {message}
            </p>
          )}
          {!isSuccess && isSubmitted && (
            <p className="form__error">
              There is some problem, user not registered!
              <br />
              {message}
            </p>
          )}
          <button className="form__submit-btn" type="submit">
            Create an account
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
