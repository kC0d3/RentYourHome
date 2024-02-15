import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function CreateAd({ loggedUser }) {
    const navigate = useNavigate();
    const [images, setImages] = useState([]);
    const [ad, setAd] = useState({
        address: {
            zipCode: "",
            city: "",
            street: "",
            houseNumber: ""
        },
        rooms: 0,
        size: 0,
        price: 0,
        description: "",
        images: [],
        userId: 0
    });

    const generateFormData = () => {
        const formData = new FormData();
        images.forEach(file => formData.append("files", file, file.filename));
        return formData;
    }

    const handleSubmit = async e => {
        e.preventDefault();

        try {
            const updatedAd = { ...ad, images: images.map(file => file.name), userId: loggedUser.id };
            setAd(updatedAd);
            const res = await fetch("/api/ads", {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(updatedAd)
            });

            if (!res.ok) {
                throw new Error(`HTTP error! status: ${res.status}`);
            }

            await fetch("/api/images", {
                method: 'POST',
                body: generateFormData()
            });

            navigate('/profile');
        }
        catch (error) {
            console.log(error)
        }
    }

    return (
        <>
            <form className='create-ad' onSubmit={handleSubmit}>
                <label>Address</label>
                <label>Zipcode</label>
                <input type='text' onChange={e => setAd({ ...ad, address: { ...ad.address, zipCode: e.target.value } })} value={ad.address.zipCode} required />
                <label>City</label>
                <input type='text' onChange={e => setAd({ ...ad, address: { ...ad.address, city: e.target.value } })} value={ad.address.city} required />
                <label>Street</label>
                <input type='text' onChange={e => setAd({ ...ad, address: { ...ad.address, street: e.target.value } })} value={ad.address.street} required />
                <label>House number</label>
                <input type='text' onChange={e => setAd({ ...ad, address: { ...ad.address, houseNumber: e.target.value } })} value={ad.address.houseNumber} required />
                <label>Rooms</label>
                <input type='number' className='no-spinners' onChange={e => setAd({ ...ad, rooms: parseInt(e.target.value) })} value={ad.rooms} required />
                <label>Size</label>
                <input type='number' className='no-spinners' onChange={e => setAd({ ...ad, size: parseInt(e.target.value) })} value={ad.size} required />
                <label>Price</label>
                <input type='number' className='no-spinners' onChange={e => setAd({ ...ad, price: parseInt(e.target.value) })} value={ad.price} required />
                <label>Description</label>
                <input type='text' onChange={e => setAd({ ...ad, description: e.target.value })} value={ad.description} required />
                <label>Add Pictures</label>
                <input type='file' id='files' multiple onChange={e => setImages(Array.from(e.target.files))} required />
                <div className="button-group">
                    <button type='submit'>Post Ad</button>
                    <button type='button' onClick={() => navigate(-1)}>Cancel</button>
                </div>
            </form>
        </>
    );
}