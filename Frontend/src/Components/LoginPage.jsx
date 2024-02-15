import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import Navbar from "./Navbar.jsx";
import RegistrationForm from "./RegistrationForm";

function LoginPage({ loggedUser, setLoggedUser }) {
    const navigate = useNavigate();
    const [showRegistration, setShowRegistration] = useState(false);
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [loginError, setLoginError] = useState(null);

    const handleLoginSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    username: username,
                    password: password,
                }),
            });
            
            console.log(username);
            console.log(password);
            let a = await response.text();
            console.log(a)

            if (response.ok && a !== "Admin") {
                const response = await fetch(`/api/users/${username}`);
                const userData = await response.json();
                await setLoggedUser(userData);
                console.log('User successfully logged in.');
                setTimeout(() => {
                navigate('/');
                }, 3000);
            } else if(response.ok && a == "Admin"){
                const admin = {username: "Admin",
                                firstName: "Admin",
                                lastName: "Admin",
                                email: "..."}    
                await setLoggedUser(admin);
                setTimeout(() => {
                    navigate('/');
                }, 1000);            
            }else{
                setLoginError('Invalid username or password. Please try agagin.');
                console.error('Invalid username or password during login.');
            }
        } catch (error) {
            setLoginError('Invalid username or password. Please try agagin.');
            console.error('Invalid username or password during login.');
        }
    }

    const handleRegisterClick = () => {
        setShowRegistration(true);
    };

    return (
        <div className="login-page">
            {loggedUser ? (
                // Show only when user is logged in
                <>
                    <h2>Welcome, {username}</h2>
                    <p>You will be redirected to the Home Page in 3..2..1..</p>
                    <div className="cancel-button">
                        <Link to="/">
                            <button>Home</button>
                        </Link>
                    </div>
                </>
            ) : (
                // Show only when user is not logged in
                <>
                    <h3>Please log in to access the full website.</h3>
                    <div className="login-form">
                        <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} />
                        <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
                        <button onClick={handleLoginSubmit}>Log in</button>
                        <p>Not a member yet? <span onClick={handleRegisterClick} style={{ textDecoration: "underline", cursor: "pointer" }}>Click here to Register</span></p>
                    </div>
                    {showRegistration && <RegistrationForm setShowRegistration={setShowRegistration} />}
                </>
            )}
        </div>
    );
}

export default LoginPage;