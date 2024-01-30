import React, { useState } from "react";
import RegistrationForm from "./RegistrationForm";

function LoginPage() {
    const [showRegistration, setShowRegistration] = useState(false);

    const handleRegisterClick = () => {
        setShowRegistration(true);
    };

    return (
        <div className="login-page">
            <div className="login-form">
                <input type="text" placeholder="Username" />
                <input type="password" placeholder="Password" />
                <button>Submit</button>
                <button onClick={handleRegisterClick}>Register</button>
            </div>

            {showRegistration && <RegistrationForm setShowRegistration={setShowRegistration} />}
        </div>
    )
}

export default LoginPage;