import React, { useState, useEffect } from "react";
import SearchBar from "./SearchBar.jsx";
import FiltersBar from "./FiltersBar.jsx";

function HomePage() {
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

    const handleSearch = (searchTerm) => {
        console.log('Search for:', searchTerm)
        // TODO: Search logic
    }

    const handleFilterSelect = (filter) => {
        console.log('Filter selected:', filter)
        // TODO: FIlter logic
    }

    return (
        <div className="homepage-container">
            <div className="content-area">
                <div className="search-filter-area">
                    <SearchBar onSearch={handleSearch} />
                    <FiltersBar onFilterSelect={handleFilterSelect} />
                    <div>{data}</div>
                </div>
            </div>
        </div>
    );
}

export default HomePage;