import Nav from './Nav/Nav';
import './Header.scss';

const Header = () => {
    return(
        <header className="header">
            <img className="header__logo" src="./assets/logo.svg" alt="Netwix company logo" />
            <Nav />
            <img className="header__avatar" src="./assets/image-avatar.png" alt="User avatar" />
        </header>
    )
}

export default Header;