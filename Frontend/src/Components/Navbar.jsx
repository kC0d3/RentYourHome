import React from "react";

function Navbar() {
    return (
        <div className="navbar">
                    <button onClick={() => window.location.href='/'}>Home</button>
                    <button onClick={() => window.location.href='/ads'}>Ads</button>
                    <button onClick={() => window.location.href='/login'}>Login/Register</button>
        </div>
    );
}

export default Navbar;