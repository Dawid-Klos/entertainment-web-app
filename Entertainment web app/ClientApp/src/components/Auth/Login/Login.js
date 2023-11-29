import {useRef} from "react";
import {Link, useNavigate} from "react-router-dom";

import logo from '../../../assets/logo.svg';
import './Login.scss';

const Login = () => {
    const email = useRef();
    const password = useRef();
    const navigate = useNavigate();
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        let res;
        
        const body = {
            "Email": email.current.value,
            "Password": password.current.value,
        }
        
        try {
            let login = await fetch('/api/Auth/Login', {
                method: 'POST',
                credentials: 'include',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(body)
            });
            
            res = await login.json();
        } catch (error) {
            console.log("Error: ", error);
        }
        
        if(res && res.isSuccess) {
            // const token = res.Message;
            // const expireDate = new Date(res.ExpireDate);

            navigate("/");
        } else {
            console.log("There is some problem, user not logged in!");
            // TODO: Show error message to the user
        }
    }
    
    return (
        <section className="login">
            <img className="login__logo" src={logo} alt="Netwix company logo"/>
            
            <div className="login__container">
            
            
                <form className="form" onSubmit={handleSubmit}>
                    <h2 className="form__title">Login</h2>
                    
                    <div className="form__inputs-wrapper">
                        <div className="input">
                            <label htmlFor="email" className="input__label">Email address</label>
                            <input className="input__input" type="text" ref={email} id="email"/>
                        </div>
                        <div className="input">
                            <label htmlFor="password" className="input__label">Password</label>
                            <input className="input__input" type="password" ref={password} id="password"/>
                        </div>
                    </div>
                    
                    <button className="form__submit-btn" type="submit">Login to your account</button>
                </form>
                <div className="create-account">
                    <p className="create-account__text">Don't have an account?</p>
                    <Link className="create-account__link" to="/register">Sign Up</Link>
                </div>
            </div>
        </section>
    )
}

export default Login;