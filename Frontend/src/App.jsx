import React, { useEffect, useState } from 'react';
import Navbar from "./Components/Navbar.jsx";
import HomePage from "./Components/HomePage.jsx";

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
      <br></br>
      <div>{data}</div>
    </>
  );
}

export default App;