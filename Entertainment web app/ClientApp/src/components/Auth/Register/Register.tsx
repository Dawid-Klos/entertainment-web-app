import { Link } from "react-router-dom";
import { useForm, SubmitHandler } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import Spinner from "@components/common/Spinner/Spinner";
import { registerSchema, RegisterBody } from "@config/formSchemas";
import { useAuth } from "@hooks/useAuth";

import logo from "@assets/logo.svg";
import "../Auth.scss";

const Register = () => {
  const { submission, sendRequest } = useAuth();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterBody>({
    resolver: zodResolver(registerSchema),
  });

  const onSubmit: SubmitHandler<RegisterBody> = async (data) => {
    const body: RegisterBody = {
      email: data.email,
      firstName: data.firstName,
      lastName: data.lastName,
      password: data.password,
      confirmPassword: data.confirmPassword,
    };

    await sendRequest(body, "/api/auth/register");
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
          <h2 className="form__title">Sign up</h2>
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
            <label htmlFor="firstname" className="input-container__label">
              Firstname
            </label>
            <input
              className="input-container__input"
              type="text"
              id="firstname"
              {...register("firstName")}
            />
            <p className="form__error">{errors.firstName?.message}</p>
          </div>
          <div className="input-container">
            <label htmlFor="firstname" className="input-container__label">
              Lastname
            </label>
            <input
              className="input-container__input"
              type="text"
              id="Lastname"
              {...register("lastName")}
            />
            <p className="form__error">{errors.lastName?.message}</p>
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
              id="confirm-password"
              {...register("confirmPassword")}
            />
            <p className="form__error">{errors.confirmPassword?.message}</p>
          </div>

          {(submission.status === "success" ||
            submission.status === "error") && (
            <p className="form__special-error">{submission.message}</p>
          )}

          <button className="form__submit-btn" type="submit">
            {submission.status === "submitting" ? "" : "Create an account"}
            <Spinner
              loading={submission.status === "submitting"}
              variant="primary"
            />
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
