import './Register.scss';
import {Link} from 'react-router-dom';
import {useRef} from 'react';

const Register = () => {

    const firstName = useRef();
    const lastName = useRef();
    const email = useRef();
    const password = useRef();
    const confirmPassword = useRef();

    const handleSubmit = async (e) => {
        e.preventDefault();
        const body = {
            "Email": email.current.value,
            "Password": password.current.value,
            "ConfirmPassword": confirmPassword.current.value,
            "Firstname": firstName.current.value,
            "Lastname": lastName.current.value
        }
        try {
            let register = await fetch('/api/Auth/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(body)
            });
            register = register.json();

            switch (register.status) {
                case 200:
                    console.log("success");
                    break;
                case 400:
                    console.log("The are some errors..");

                    break;
                case 500:
                    console.log("Internal server error.. :(");
                    break;
                default:
                    console.log("Something went wrong. Try again.");
            }
            const errors = {...register.errors};
            console.log(errors);
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <section className="register">
            <img className="register__logo" src="./assets/logo.svg" alt="Netwix company logo"/>
            <form className="register__form" onSubmit={handleSubmit}>
                <h2 className="register__form--title">Sign up</h2>
                <div className="input-container">
                    <label htmlFor="email" className="input-container__label">Email address</label>
                    <input className="input-container__input" type="text" ref={email} name="email" id="email"/>
                </div>
                <div className="input-container">
                    <label htmlFor="firstname" className="input-container__label">Firstname</label>
                    <input className="input-container__input" type="text" ref={firstName} name="firstname"
                           id="firstname"/>
                </div>
                <div className="input-container">
                    <label htmlFor="firstname" className="input-container__label">Lastname</label>
                    <input className="input-container__input" type="text" ref={lastName} name="Lastname" id="Lastname"/>
                </div>
                <div className="input-container">
                    <label htmlFor="password" className="input-container__label">Password</label>
                    <input className="input-container__input" type="password" ref={password} name="password"
                           id="password"/>
                </div>
                <div className="input-container">
                    <label htmlFor="confirm-password" className="input-container__label">Confirm Password</label>
                    <input className="input-container__input" type="password" ref={confirmPassword}
                           id="confirm-password"/>
                </div>
                {/*{ errors ?  }*/}
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