import React from "react";
import ProfileInfo from "./ProfileInfo";
import ProfileAds from "./ProfilAds";

function ProfilePage({loggedUser}) {
console.log(loggedUser)

    return (
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
        </>
    )
}

export default ProfilePage;