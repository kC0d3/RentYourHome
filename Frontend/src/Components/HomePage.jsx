import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Navbar from "./Navbar.jsx";
import FiltersBar from "./FiltersBar.jsx";

function HomePage() {
    const navigate = useNavigate();
    const [allAdsData, setAllAdsData] = useState([]); // before filtering
    const [displayedAds, setDisplayedAds] = useState([]); // after filtering
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
        fetch('/api/ads')
            .then(response => response.json())
            .then(data => {
                setAllAdsData(data);
                setDisplayedAds(data);
                console.log(data);
            })
            .catch(error => console.log("Error fetching ads:", error));
    }, []);

    const handleFilterChange = (filterType, value) => {
        setFilters(prevFilters => ({
            ...prevFilters, [filterType]: value
        }));
        console.log('Filter selected:', filterType, value)
    }

    const handleApplyFilters = () => {
        let filteredAds = allAdsData;

        if (filters.location) {
            filteredAds = filteredAds.filter(ad => 
                ad.address.city.toLowerCase().includes(filters.location.toLowerCase()) ||
                ad.address.street.toLowerCase().includes(filters.location.toLowerCase()) ||
                ad.address.zipCode.toLowerCase().includes(filters.location));
        }
        setDisplayedAds(filteredAds);
    }
    

    return (
        <>
            <Navbar />
            <div className="homepage-container">
                    <div className="filter-area">
                        <FiltersBar onFilterChange={handleFilterChange} onSubmitFilters={handleApplyFilters} />
                        <br></br>
                            </div>
                <div className="content-area">
                            {displayedAds.map((ad, index) => (
                                <div key={index} className="card" onClick={() => navigate(`/ads/${ad.id}`)}>
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