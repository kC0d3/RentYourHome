import React from "react";
import ProfileInfo from "./ProfileInfo";
import ProfileAds from "./ProfilAds";
import { useState, useEffect } from "react";

function ProfilePage({loggedUser, setLoggedUser, role}) {
console.log(loggedUser)

const [unApprovedAds, setUnApprovedAds] = useState([]);
const [approvedAds, setApprovedAds] = useState([]);
const [applied, setApplied] = useState([])

useEffect(() => {
    fetchData();
}, []);

const fetchData = async () => {
    if(role !== "Admin"){
    try {
        const userResponse = await fetch(`/api/users/${loggedUser.username}`);
        const userData = await userResponse.json();
        setLoggedUser(userData);
        
    } catch (error) {
        console.log("Error fetching data:", error);
    }}
    else{
        try {
            
            const adsResponse = await fetch('/api/ads');
            const data = await adsResponse.json();
        
            let unApprovedAds = data.filter(ad => !ad.approved);
            let approvedAds = data.filter(ad => ad.approved);
            setUnApprovedAds(unApprovedAds);
            setApprovedAds(approvedAds);
        } catch (error) {
            console.log("Error fetching data:", error);
        }        
    }
};

    return (
        <>{loggedUser.username == "Admin" ? (
        <><ProfileInfo loggedUser={loggedUser}/>
            <div className="profil-page-container">
            <div className="published-ads-container">
                <h2 className="published-ads-title">Approved Ads</h2>
                    <ProfileAds className="published-ads" ads={approvedAds} isAdmin={role === "Admin"} setApprovedAds={setApprovedAds} setUnApprovedAds={setUnApprovedAds}/>
            </div>
            <div className="applied-ads-container">
                <h2 className="applied-ads-title">UnApproved Ads</h2>
                    <ProfileAds className="published-ads" ads={unApprovedAds} isAdmin={role === "Admin"} setApprovedAds={setApprovedAds} setUnApprovedAds={setUnApprovedAds}/>
            </div>
        </div>
            
        </>) : (
        <>
        <ProfileInfo loggedUser={loggedUser}/>
        <div className="profil-page-container">
            <div className="published-ads-container">
                <h2 className="published-ads-title">Published Ads</h2>
                    <ProfileAds className="published-ads" ads={loggedUser.publishedAds} setApprovedAds={setApprovedAds} setUnApprovedAds={setUnApprovedAds} username={loggedUser.username} setLoggedUser={setLoggedUser}/>
            </div>
            <div className="applied-ads-container">
                <h2 className="applied-ads-title">Applied Ads</h2>
                    <ProfileAds className="published-ads" ads={applied} setApprovedAds={setApprovedAds} setUnApprovedAds={setUnApprovedAds} username={loggedUser.username} setLoggedUser={setLoggedUser}/>
            </div>
        </div>
        </>)
        }
        
        </>
    )
}

export default ProfilePage;