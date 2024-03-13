import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Navbar from "./Navbar.jsx";
import FiltersBar from "./FiltersBar.jsx";

function HomePage() {
    const navigate = useNavigate();
    const [allAdsData, setAllAdsData] = useState([]); // before filtering
    const [displayedAds, setDisplayedAds] = useState([]); // after filtering
    const [approvedAds, setApprovedAds] = useState([]);
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
                let approvedAds = data.filter(ad => ad.approved == true)
                console.log(approvedAds);
                setDisplayedAds(approvedAds);
                setApprovedAds(approvedAds)
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
        let filteredAds = approvedAds;

        if (filters.location) {
            filteredAds = filteredAds.filter(ad =>
                ad.address.city.toLowerCase().includes(filters.location.toLowerCase()) ||
                ad.address.street.toLowerCase().includes(filters.location.toLowerCase()) ||
                ad.address.zipCode.toLowerCase().includes(filters.location));
        }

        if (filters.minPrice) {
            filteredAds = filteredAds.filter(ad =>
                ad.price >= filters.minPrice);
        }

        if (filters.maxPrice) {
            filteredAds = filteredAds.filter(ad =>
                ad.price <= filters.maxPrice);
        }

        if (filters.minSize) {
            filteredAds = filteredAds.filter(ad =>
                ad.size >= filters.minSize);
        }

        if (filters.maxSize) {
            filteredAds = filteredAds.filter(ad =>
                ad.size <= filters.maxSize);
        }

        if (filters.minRooms) {
            filteredAds = filteredAds.filter(ad =>
                ad.rooms >= filters.minRooms);
        }

        if (filters.maxRooms) {
            filteredAds = filteredAds.filter(ad =>
                ad.rooms <= filters.maxRooms);
        }
        setDisplayedAds(filteredAds);
    }

    const handleResetFilters = () => {
        setFilters({
            location: '',
            minPrice: '',
            maxPrice: '',
            minSize: '',
            maxSize: '',
            minRooms: '',
            maxRooms: '',
        });
        setDisplayedAds(approvedAds);
    }

    return (
        <div className="homepage-container">
            <div className="filter-area">
                <FiltersBar
                    onFilterChange={handleFilterChange}
                    onSubmitFilters={handleApplyFilters}
                    onResetFilters={handleResetFilters}
                    filters={filters}
                />
                <br></br>
            </div>
            <div className="content-area">
                {displayedAds.map((ad, index) => (
                    <div key={index} className="card" onClick={() => navigate(`/ads/${ad.id}`)}>
                        <div className="image-container">
                            {ad.images[0] && (
                                <img src={`/api/images/${ad.images[0]}`} alt="Ad" />
                            )}
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
    );
}

export default HomePage;