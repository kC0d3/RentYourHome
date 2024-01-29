import React from "react";
import './Navbar.css';

function Navbar() {
    return(
        <nav>
            <ul>
                <li><a href="/">Home</a></li>
                <li><a href="/ads">Ads</a></li>
                <li><a href="/login">Login/Register</a></li>
            </ul>
        </nav>
    );
}

export default Navbar;