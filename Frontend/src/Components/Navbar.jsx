import { Link } from "react-router-dom";

function Navbar({ loggedUser, onLogout }) {
    return (
        <div className="navbar">
            <Link to="/">
                <button>Home</button>
            </Link>
            {loggedUser ? (
                <>
                    <Link to="/profile">
                        <button>Post Ad</button>
                    </Link>
                    <Link to="/profile">
                        <button>Profile</button>
                    </Link>
                    <Link to="/login">
                        <button onClick={onLogout}>Logout</button>
                    </Link>
                </>
            ) : (
                <>
                    <Link to="/login">
                        <button>Login/Register</button>
                    </Link>
                </>
            )}

        </div>
    );
}

export default Navbar;