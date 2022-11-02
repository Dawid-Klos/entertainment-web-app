import './Login.scss';
import {Link} from "react-router-dom";

const Login = () => {
    
    return(
        <section className="login">
            <img className="login__logo" src="./assets/logo.svg" alt="Netwix company logo" />
            <form className="login__form">
                <h2 className="login__form--title">Login</h2>
                <div className="input-container">
                    <label htmlFor="email" className="input-container__label">Email address</label>
                    <input className="input-container__input" type="text" id="email"/>
                </div>
                <div className="input-container">
                    <label htmlFor="password" className="input-container__label">Password</label>
                    <input className="input-container__input" type="text" id="password" />
                </div>
                <button className="login__form--submit-btn" type="submit">Login to your account</button>
                <div className="create-account">
                    <p className="create-account__text">Don't have an account?</p>
                    <Link className="create-account__link" to="/register">Sign Up</Link>
                </div>
            </form>
        </section>
    )
}

export default Login;