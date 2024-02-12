import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

function AdDetails() {
    const { id } = useParams(); // Get ID from URL parameter
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

    // Check if images is an array and has items
    const hasImages = Array.isArray(adDetails.images) && adDetails.images.length > 0;

    return (
        <div className="ad-details">
            <h1>{adDetails.title}</h1>
            {hasImages && (
                <div className="image-container">
                    {adDetails.images.map((imageName, index) => (
                        <img key={index} src={`/api/images/${imageName}`} alt={`Ad image ${index + 1}`} />
                    ))}
                </div>
            )}
            <div>Location: {`${adDetails.address.zipCode}, ${adDetails.address.city}, ${adDetails.address.street} ${adDetails.address.houseNumber}`}</div>
            <div>Rooms: {adDetails.rooms}</div>
            <div>Size: {adDetails.size} sqm</div>
            <div>Price: {adDetails.price} HUF</div>
            <div>Description: {adDetails.description}</div>
        </div>
    )
}

export default AdDetails;