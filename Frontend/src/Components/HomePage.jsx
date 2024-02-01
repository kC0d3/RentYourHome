import { useState, useEffect } from "react";
import Navbar from "./Navbar.jsx";
import FiltersBar from "./FiltersBar.jsx";

function HomePage() {
    const [adsData, setAdsData] = useState([]);
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

        fetch(`/api/ads/all?${queryParams}`)
            .then(response => response.json())
            .then(data => {
                setAdsData(data);
                console.log(data);
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
                    <div className="filter-area">
                        <FiltersBar onFilter={handleFilterChange} />
                        <br></br>
                            </div>
                <div className="content-area">
                            {adsData.map((ad, index) => (
                                <div key={index} className="card">
                                    <div className="image-container">
                                        {ad.images.map((imageName, index) => (
                                            <img key={index} src={`/api/images/${imageName}`} alt="Ad" />
                                        ))}
                                    </div>
                                    <div>Location: {`${ad.address.zipCode}, ${ad.address.city}, ${ad.address.street} ${ad.address.houseNumber}`}</div>
                                    <div>Rooms: {ad.rooms}</div>
                                    <div>Size: {ad.size} sqm</div>
                                    <div>Price: {ad.price} HUF</div>
                                    <div>Description: {ad.description}</div>
                                    <br></br>
                                </div>
                            ))}
                    </div>
                </div>
        </>
    );
}

export default HomePage;