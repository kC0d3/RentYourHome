import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";

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
        return <div><h2>Loading...</h2></div>;
    }

    const imageGalleryClass = `image-gallery ${adDetails.images.length === 1 ? "one-image" :
            adDetails.images.length === 2 ? "two-images" :
                adDetails.images.length === 3 ? "three-images" :
                    "multi-image"
        }`;

    return (
        <div className="ad-details">
            <div className={imageGalleryClass}>
                {adDetails.images.map((imageName, index) => {
                    return <img key={index} src={`/api/images/${imageName}`} alt={`Ad image ${index + 1}`} />
                })}
            </div>
            <div className="detailed-info">
                <h1>{adDetails.title}</h1>
                <div>Location: {`${adDetails.address.zipCode}, ${adDetails.address.city}, ${adDetails.address.street} ${adDetails.address.houseNumber}`}</div>
                <div>Rooms: {adDetails.rooms}</div>
                <div>Size: {adDetails.size} sqm</div>
                <div>Price: {adDetails.price} HUF</div>
                <div>Description: {adDetails.description}</div>
            </div>
            <div className="cancel-button">
                <Link to="/">
                    <button>&times;</button>
                </Link>
            </div>
        </div>
    )
}

export default AdDetails;