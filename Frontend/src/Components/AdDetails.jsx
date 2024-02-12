import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

function AdDetails() {
    const { id } = useParams; // Get ID from URL parameter
    const [adDetails, setAdDetails] = useState(null);

    useEffect(() => {
        fetch(`/api/ads/${id}`)
            .then(res => res.json())
            .then(data => {
                setAdDetails(data);
            })
            .catch(err => {
                console.error("Error fetching ad details:", err);
            });
    }, [id]); // Run effect when ID changes

    if (!adDetails) {
        return <div>Loading...</div>;
    }

    return (
        <div className="ad-details">
            <h1>{adDetails.title}</h1>
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
        </div>
    )
}

export default AdDetails;