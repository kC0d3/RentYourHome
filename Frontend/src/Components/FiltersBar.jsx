import React from "react";

function FiltersBar({ onFilter }) {
    return (
        <div className="filters">
            <div className="filter-input-group">
                <input
                    type="text"
                    placeholder="Location"
                    onChange={(e) => onFilter('location', e.target.value)}
                    className="filter-input location-input"
                />
            </div>
            <div className="filter-input-group">
                <input
                    type="number"
                    placeholder="Min Price"
                    onChange={(e) => onFilter('minPrice', e.target.value)}
                    className="filter-input"
                />
                <input
                    type="number"
                    placeholder="Max Price"
                    onChange={(e) => onFilter('maxPrice', e.target.value)}
                    className="filter-input"
                />
            </div>
            <div className="filter-input-group">
                <input
                    type="number"
                    placeholder="Min Size"
                    onChange={(e) => onFilter('minSize', e.target.value)}
                    className="filter-input"
                />
                <input
                    type="number"
                    placeholder="Max Size"
                    onChange={(e) => onFilter('maxSize', e.target.value)}
                    className="filter-input"
                />
            </div>
            <div className="filter-input-group">
                <input
                    type="number"
                    placeholder="Min Rooms"
                    onChange={(e) => onFilter('minRooms', e.target.value)}
                    className="filter-input"
                />
                <input
                    type="number"
                    placeholder="Max Rooms"
                    onChange={(e) => onFilter('maxRooms', e.target.value)}
                    className="filter-input"
                />
            </div>
        </div>
    );
}

export default FiltersBar;