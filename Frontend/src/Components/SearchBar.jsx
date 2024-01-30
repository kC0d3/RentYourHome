import React, { useState } from "react";

function SearchBar({ onSearch }) {
    const [searchTerm, setSearchTerm] = useState('');

    const handleSearch = (searchTerm) => {
        console.log('Search for:', searchTerm)
        // TODO: Search logic
    }

    return (
        <div className="search-bar">
            <input
                type="text"
                placeholder="Search..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="search-input"
                />
            <button onClick={handleSearch} className="search-button">Search</button>
        </div>
    );
}

export default SearchBar;