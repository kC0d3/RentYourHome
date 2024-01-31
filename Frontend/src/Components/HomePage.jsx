import { useState, useEffect } from "react";
import Navbar from "./Navbar.jsx";
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
        <>
            <Navbar />
            <div className="homepage-container">
                <div className="content-area">
                    <div className="filter-area">
                        <FiltersBar onFilter={handleFilterChange} />
                        <br></br>
                        <div className="data-area">{data}</div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default HomePage;