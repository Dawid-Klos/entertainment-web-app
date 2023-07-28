import React from "react";
import {useRef} from "react";
import {Link, useNavigate} from "react-router-dom";

import './Login.scss';

import { useSignIn } from 'react-auth-kit';

const Login = () => {
    const email = useRef();
    const password = useRef();
    const navigate = useNavigate();
    const signIn = useSignIn();
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        
        const body = {
            "Email": email.current.value,
            "Password": password.current.value,
        }
        
        let res;
        
        try {
            let login = await fetch('/api/Auth/Login', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(body)
            });
            login = await login.json();
            res = login;
        } catch (error) {
            console.log("Error: ", error);
        }
        
        console.log("response: ", res);
        
        if(res && res.isSuccess) {
            
            let dateNow = new window.Date();
            let expiryDate = new window.Date(res.ExpireDate);
            
            let expiryDateInSeconds = (dateNow.getTime() - expiryDate.getTime()) / 1000;
            Math.trunc(expiryDateInSeconds);
            
            signIn({
                    token: res.Message,
                    expiresIn: expiryDateInSeconds / 60,
                    tokenType: "Bearer",
                    authState: { email: email.current.value, authenticated: true }
            });
            
            // Redirect or do-something
            setTimeout(() => { navigate("/"); }, 100);
            console.log("success");
        } else {
            //Throw error
            console.log("There is some problem, user not logged in!");
        }
    }
    
    return (
        <section className="login">
            <img className="login__logo" src="./assets/logo.svg" alt="Netwix company logo"/>
            <form className="login__form" onSubmit={handleSubmit}>
                <h2 className="login__form--title">Login</h2>
                <div className="input-container">
                    <label htmlFor="email" className="input-container__label">Email address</label>
                    <input className="input-container__input" type="text" ref={email} id="email"/>
                </div>
                <div className="input-container">
                    <label htmlFor="password" className="input-container__label">Password</label>
                    <input className="input-container__input" type="password" ref={password} id="password"/>
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