import { Link } from "react-router-dom";

function Navbar({ loggedUser, setLoggedUser }) {

    const handleLogout = () => {
        fetch('/api/auth/logout', { // call logout endpoint
            method: 'POST'
        })
            .then(() => {
                setLoggedUser(undefined); // reset user state
            })
            .catch(err => console.error('Logout error', err));
    };

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
                    <Link to="/">
                        <button onClick={handleLogout}>Logout</button>
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