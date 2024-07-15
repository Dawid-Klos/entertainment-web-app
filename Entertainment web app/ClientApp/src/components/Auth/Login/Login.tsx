import { Link } from "react-router-dom";
import { SubmitHandler, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import Spinner from "@components/common/Spinner/Spinner";

import { useAuth } from "@hooks/useAuth";
import { loginSchema, LoginBody } from "@config/formSchemas";

import logo from "@assets/logo.svg";
import "../Auth.scss";

const Login = () => {
  const { submission, sendRequest } = useAuth();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginBody>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit: SubmitHandler<LoginBody> = async (data) => {
    const body: LoginBody = {
      email: data.email,
      password: data.password,
    };

    await sendRequest(body, "/api/auth/login");
  };

  return (
    <section className="auth-section">
      <img
        className="auth-section__logo"
        src={logo}
        alt="Netwix company logo"
      />

      <div className="auth-section__container">
        <form className="form" onSubmit={handleSubmit(onSubmit)}>
          <h2 className="form__title">Login</h2>

          <div className="form__inputs-wrapper">
            <div className="input-container">
              <label htmlFor="email" className="input-container__label">
                Email address
              </label>
              <input
                className="input-container__input"
                type="text"
                id="email"
                {...register("email")}
              />
              <p className="form__error">{errors.email?.message}</p>
            </div>
            <div className="input-container">
              <label htmlFor="password" className="input-container__label">
                Password
              </label>
              <input
                className="input-container__input"
                type="password"
                id="password"
                {...register("password")}
              />
              <p className="form__error">{errors.password?.message}</p>
            </div>
          </div>

          {(submission.status === "success" ||
            submission.status === "error") && (
            <p className="form__special-error">{submission.message}</p>
          )}

          <button className="form__submit-btn" type="submit">
            {submission.status === "submitting" ? "" : "Login to your account"}
            <Spinner
              loading={submission.status === "submitting"}
              variant="primary"
            />
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
