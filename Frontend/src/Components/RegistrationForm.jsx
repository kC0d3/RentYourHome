import React from "react";

function RegistrationForm({ setShowRegistration }) {
    const handleClose = () => {
        setShowRegistration(false);
    };

    return (
        <div className="registration-popup">
            <div className="registration-content">
                <span className="close" onClick={handleClose}>&times;</span>
                <input type="text" placeholder="FirstName" />
                <input type="text" placeholder="LastName" />
                <input type="text" placeholder="Email" />
                <input type="text" placeholder="UserName" />
                <input type="text" placeholder="Password" />
                <button>Submit</button>
            </div>
        </div>
    );
}

export default RegistrationForm;