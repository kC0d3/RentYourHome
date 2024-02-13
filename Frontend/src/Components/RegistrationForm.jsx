import { useState } from "react";
import { Link } from "react-router-dom";

function RegistrationForm({ setShowRegistration }) {
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [registrationMessage, setRegistrationMessage] = useState('');

    const handleClose = () => {
        setShowRegistration(false);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('/api/auth/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    email,
                    username,
                    password,
                }),
            });

            if (response.status !== 400) {
                console.log('User successfully registered.');
                await fetch('/api/users', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        username,
                        firstName,
                        lastName,
                        email,
                    }),
                });
                setRegistrationMessage('Registration successful! Please proceed to log in.');
                setTimeout(() => {
                    handleClose();
                }, 5000);

            } else {
                console.error('Registration failed.');
                setRegistrationMessage('Registration failed. Please try again!');
            }
        } catch (error) {
            console.error('Failed to register user.');
            setRegistrationMessage('Registration failed. Please try again!');
        }
    }

    return (
        <div className="registration-popup">
            <div className="registration-content">
                <button className="close-button" onClick={handleClose}>&times;</button>
                    <input type="text" placeholder="FirstName"  value={firstName} onChange={(e) => setFirstName(e.target.value)} />
                    <input type="text" placeholder="LastName" value={lastName} onChange={(e) => setLastName(e.target.value)} />
                    <input type="text" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                    <input type="text" placeholder="UserName" value={username} onChange={(e) => setUsername(e.target.value)} />
                    <input type="password" placeholder="Password"value={password} onChange={(e) => setPassword(e.target.value)} />
                    <button onClick={handleSubmit}>Submit</button>
            </div>
            <div className="cancel-button">
                <button onClick={handleClose}>Cancel</button>
                <Link to="/">
                    <button>Home</button>
                </Link>
                <div>{registrationMessage}</div>
            </div>
        </div>
    );
}

export default RegistrationForm;