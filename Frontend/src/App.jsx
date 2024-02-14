import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import HomePage from "./Components/HomePage.jsx";
import LoginPage from './Components/LoginPage.jsx';
import AdDetails from './Components/AdDetails.jsx';
import Navbar from './Components/Navbar.jsx';

function App() {
  const [loggedUser, setLoggedUser] = useState(undefined);

  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/ads/:id" element={<AdDetails />} />
      </Routes>
    </Router>
  );
}

export default App;