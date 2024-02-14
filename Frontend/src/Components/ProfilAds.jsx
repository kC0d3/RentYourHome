function ProfileAds({ads}){

    return (
        <div className="published-ads">
                {ads.length > 0 ? (
                    ads.map((ad, index) => (
                        <div key={index} className="card">
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
                    ))
                ) : (
                    <p></p>
                )}
            </div>
    )
}

export default ProfileAds;