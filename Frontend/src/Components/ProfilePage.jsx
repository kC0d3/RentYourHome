import React from "react";
import ProfileInfo from "./ProfileInfo";

function ProfilePage({loggedUser}) {
console.log(loggedUser.firstName)

    return (
        <>
        <ProfileInfo loggedUser={loggedUser}/>
        </>
    )
}

export default ProfilePage;