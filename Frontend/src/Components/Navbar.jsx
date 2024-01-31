import { Link } from "react-router-dom";

function Navbar() {
    return (
        <div className="navbar">
            <Link to="/">
                <button>Home</button>
            </Link>
            <Link to="/ads">
                <button>Ads</button>
            </Link>
            <Link to="/login">
                <button>Login/Register</button>
            </Link>
        </div>
    );
}

export default Navbar;