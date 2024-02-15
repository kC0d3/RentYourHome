import React from "react";
import ProfileInfo from "./ProfileInfo";
import ProfileAds from "./ProfilAds";
import { useState, useEffect } from "react";

function ProfilePage({loggedUser}) {
console.log(loggedUser)

const [unApprovedAds, setUnApprovedAds] = useState([]);
const [approvedAds, setApprovedAds] = useState([]);

useEffect(() => {
    fetch('/api/ads')
        .then(response => response.json())
        .then(data => {
            let unApprovedAds = data.filter(ad=> ad.approved == false)
            let approvedAds = data.filter(ad=> ad.approved == true)            
            setUnApprovedAds(unApprovedAds)
            setApprovedAds(approvedAds)            
        })
        .catch(error => console.log("Error fetching ads:", error));
}, []);

    return (
        <>{loggedUser.username == "Admin" ? (
        <><ProfileInfo loggedUser={loggedUser}/>
            <div className="profil-page-container">
            <div className="published-ads-container">
                <h2 className="published-ads-title">Approved Ads</h2>
                    <ProfileAds className="published-ads" ads={approvedAds}/>
            </div>
            <div className="applied-ads-container">
                <h2 className="applied-ads-title">UnApproved Ads</h2>
                    <ProfileAds className="published-ads" ads={unApprovedAds} isAdmin={loggedUser.username === "Admin"} setApprovedAds={setApprovedAds} setUnApprovedAds={setUnApprovedAds}/>
            </div>
        </div>
            
        </>) : (
        <>
        <ProfileInfo loggedUser={loggedUser}/>
        <div className="profil-page-container">
            <div className="published-ads-container">
                <h2 className="published-ads-title">Published Ads</h2>
                    <ProfileAds className="published-ads" ads={loggedUser.publishedAds}/>
            </div>
            <div className="applied-ads-container">
                <h2 className="applied-ads-title">Applied Ads</h2>
                    <ProfileAds className="published-ads" ads={loggedUser.publishedAds}/>
            </div>
        </div>
        </>)
        }
        
        </>
    )
}

export default ProfilePage;