import React from "react";
import { Link } from "react-router-dom";

function RegistrationForm({ setShowRegistration }) {
    const handleClose = () => {
        setShowRegistration(false);
    };

    return (
        <div className="registration-popup">
            <div className="registration-content">
                <button className="close-button" onClick={handleClose}>&times;</button>
                <input type="text" placeholder="FirstName" />
                <input type="text" placeholder="LastName" />
                <input type="text" placeholder="Email" />
                <input type="text" placeholder="UserName" />
                <input type="text" placeholder="Password" />
                <button>Submit</button>
            </div>
            <div className="cancel-button">
                <Link to="/">
                    <button>Cancel</button>
                </Link>
            </div>
        </div>
    );
}

export default RegistrationForm;