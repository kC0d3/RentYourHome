import React from "react";
import SearchBar from "./SearchBar.jsx";
import FiltersBar from "./FiltersBar.jsx";

function HomePage() {
    const handleSearch = (searchTerm) => {
        console.log('Search for:', searchTerm)
        // TODO: Search logic
    }

    const handleFilterSelect = (filter) => {
        console.log('Filter selected:', filter)
        // TODO: FIlter logic
    }

    return (
        <div>
            <SearchBar onSearch={handleSearch} />
            <FiltersBar onFilterSelect={handleFilterSelect}/>
        </div>
    );
}

export default HomePage;