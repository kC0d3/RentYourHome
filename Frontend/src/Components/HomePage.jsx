import React, { useState, useEffect } from "react";
import FiltersBar from "./FiltersBar.jsx";

function HomePage() {
    const [data, setData] = useState("");
    const [filters, setFilters] = useState({
        location: '',
        minPrice: '',
        maxPrice: '',
        minSize: '',
        maxSize: '',
        minRooms: '',
        maxRooms: '',
    });
    
    useEffect(() => {
        const queryParams = new URLSearchParams(filters).toString();

        fetch(`/api/users?${queryParams}`)
            .then(response => response.text())
            .then(d => {
                setData(d);
                console.log(d);
            }
            )
            .catch(error => console.log(error))
    }, [filters]);

    const handleFilterChange = (filterType, value) => {
        setFilters(prevFilters => ({
            ...prevFilters, [filterType]: value
        }));
        console.log('Filter selected:', filterType, value)
    }

    return (
        <div className="homepage-container">
            <div className="content-area">
                <div className="search-filter-area">
                    {/* <SearchBar onSearch={handleSearch} /> */}
                    <FiltersBar onFilter={handleFilterChange} />
                    <br></br>
                    <div>{data}</div>
                </div>
            </div>
        </div>
    );
}

export default HomePage;