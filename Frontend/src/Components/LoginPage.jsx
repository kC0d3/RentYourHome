import { useState } from "react";
import { Link } from "react-router-dom";
import Navbar from "./Navbar.jsx";
import RegistrationForm from "./RegistrationForm";

function LoginPage() {
    const [showRegistration, setShowRegistration] = useState(false);
    const [username, setUsername] = useState("");
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const handleLoginSubmit = () => {
        // TODO: Check credentials / Authorize
        setIsLoggedIn(true)
    }

    const handleRegisterClick = () => {
        setShowRegistration(true);
    };

    return (
        <>
            <Navbar />
            <div className="login-page">
                {isLoggedIn ? (<p>Welcome, {username}</p>) : (<p>Please log in to access the full website.</p>)}
                <div className="login-form">
                    <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} />
                    <input type="password" placeholder="Password" />
                    <button>Log in</button>
                    <p>Not a member yet? <span onClick={handleRegisterClick} style={{ textDecoration: "underline", cursor: "pointer" }}>Click here to Register</span></p>
                </div>
                {showRegistration && <RegistrationForm setShowRegistration={setShowRegistration} />}
                <div className="cancel-button">
                    <Link to="/">
                        <button>Home</button>
                    </Link>
                </div>
            </div>
        </>
    )
}

export default LoginPage;