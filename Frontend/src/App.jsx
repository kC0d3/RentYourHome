import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import HomePage from "./Components/HomePage.jsx";
import LoginPage from './Components/LoginPage.jsx';
import AdDetails from './Components/AdDetails.jsx';
import Navbar from './Components/Navbar.jsx';
import ProfilePage from './Components/ProfilePage.jsx';
import CreateAd from './Components/CreateAd.jsx';

function App() {
  const [loggedUser, setLoggedUser] = useState(undefined);

  return (
    <Router>
      <Navbar {...{ loggedUser, setLoggedUser }} />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage {...{ loggedUser, setLoggedUser }} />} />
        <Route path="/profile" element={<ProfilePage {...{ loggedUser, setLoggedUser }} />} />
        <Route path="/ads/:id" element={<AdDetails {...{ setLoggedUser }} />} />
        <Route path="/ads/create" element={<CreateAd {...{ loggedUser, setLoggedUser }} />} />
        {/* <Route path="*" element={<ErrorPage />} /> */}
      </Routes>
    </Router>
  );
}

export default App;