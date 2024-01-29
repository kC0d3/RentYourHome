import React, { useEffect, useState } from 'react';
import Navbar from "./Navbar/Navbar.jsx";
import HomePage from "./HomePage/HomePage.jsx";

function App() {

  const [data, setData] = useState("");

  useEffect(() => {
    fetch('/api/users')
      .then(response => response.text())
      .then(d => {
        setData(d);
        console.log(d);
      }
      )
      .catch(error => console.log(error))
  }, []);

  return (
    <>
      <Navbar />
      <HomePage />
      <div>{data}</div>
    </>
  );
}

export default App;