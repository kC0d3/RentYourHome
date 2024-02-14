function ProfileInfo({ loggedUser }) {

    
    return (
        <div className="profilebar">
            <p><span>Name:</span> {loggedUser.firstName} {loggedUser.lastName}</p>
            <p><span>Username:</span> {loggedUser.username}</p>
            <p><span>Email:</span> {loggedUser.email}</p>
        </div>
    );
}

export default ProfileInfo;