import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";

function AdDetails({ loggedUser, setLoggedUser, role }) {
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

    const fetchData = async () => {
        try {
            const res = await fetch(`/api/users/${loggedUser.username}`);
            const data = await res.json();
            setLoggedUser(data);
        } catch (error) {
            console.log("Error fetching data:", error);
        }
    };

    const handleApplyAd = adId => {
        fetch(`/api/useradapplication/apply/${adId}&${loggedUser.id}`, {
            method: 'POST'
        })
            .then(res => {
                if (res.ok) {
                    fetchData();
                }
                else {
                    console.error(`Error apply ad.`);
                }
            })
            .catch(error => console.log("Error apply ad:", error));
    }

    const handleCancelApply = adId => {
        fetch(`/api/useradapplication/cancel/${adId}&${loggedUser.id}`, {
            method: 'DELETE'
        })
            .then(res => {
                if (res.ok) {
                    fetchData();
                }
                else {
                    console.error(`Error cancel apply.`);
                }
            })
            .catch(error => console.log("Error cancel apply:", error));
    }

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
                {role !== "Admin" && loggedUser && !loggedUser.publishedAds.some(publishedAd => publishedAd.id === adDetails.id) ? !loggedUser.userAdApplications.some(applicaton => applicaton.adId === adDetails.id) ? <button onClick={() => handleApplyAd(adDetails.id)}>Apply</button> : <button onClick={() => handleCancelApply(adDetails.id)}>Cancel</button> : null}
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