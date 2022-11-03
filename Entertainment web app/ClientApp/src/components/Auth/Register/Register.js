import './Register.scss';
import {Link} from "react-router-dom";

const Register = () => {
    
    const handleSubmit = (event) => {
        event.preventDefault();
        
        const data = new FormData(event.currentTarget);
        const values = Object.fromEntries(data.entries());
        console.log(values);
    }
    
    return(
        <section className="register">
            <img className="register__logo" src="./assets/logo.svg" alt="Netwix company logo" />
            <form className="register__form" onSubmit={handleSubmit}>
                <h2 className="register__form--title">Sign up</h2>
                <div className="input-container">
                    <label htmlFor="email" className="input-container__label">Email address</label>
                    <input className="input-container__input" type="text" name="email" id="email"/>
                </div>
                <div className="input-container">
                    <label htmlFor="password" className="input-container__label">Password</label>
                    <input className="input-container__input" type="password" name="password" id="password" />
                </div>
                <div className="input-container">
                    <label htmlFor="confirm-password" className="input-container__label">Confirm Password</label>
                    <input className="input-container__input" type="password" id="confirm-password" />
                </div>
                <button className="register__form--submit-btn" type="submit">Create an account</button>
                <div className="create-account">
                    <p className="create-account__text">Already have an account?</p>
                    <Link className="create-account__link" to="/login">Login</Link>
                </div>
            </form>
        </section>
    )
}

export default Register;