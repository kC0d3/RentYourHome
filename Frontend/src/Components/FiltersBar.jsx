import React from "react";

const FiltersBar = ({ onFilterChange, onSubmitFilters, onResetFilters }) => {
    return (
        <div className="filters">
            <div className="filter-input-group">
                <input
                    type="text"
                    placeholder="Location"
                    onChange={(e) => onFilterChange('location', e.target.value)}
                    className="filter-input location-input"
                />
            </div>
            <div className="filter-input-group">
                <input
                    type="number"
                    placeholder="Min Price"
                    onChange={(e) => onFilterChange('minPrice', e.target.value)}
                    className="filter-input"
                />
                <input
                    type="number"
                    placeholder="Max Price"
                    onChange={(e) => onFilterChange('maxPrice', e.target.value)}
                    className="filter-input"
                />
            </div>
            <div className="filter-input-group">
                <input
                    type="number"
                    placeholder="Min Size"
                    onChange={(e) => onFilterChange('minSize', e.target.value)}
                    className="filter-input"
                />
                <input
                    type="number"
                    placeholder="Max Size"
                    onChange={(e) => onFilterChange('maxSize', e.target.value)}
                    className="filter-input"
                />
            </div>
            <div className="filter-input-group">
                <input
                    type="number"
                    placeholder="Min Rooms"
                    onChange={(e) => onFilterChange('minRooms', e.target.value)}
                    className="filter-input"
                />
                <input
                    type="number"
                    placeholder="Max Rooms"
                    onChange={(e) => onFilterChange('maxRooms', e.target.value)}
                    className="filter-input"
                />
            </div>
            <div>
                <button onClick={onSubmitFilters}>Apply Filters</button>
                <button onClick={onResetFilters}>Clear Filters</button>
            </div>
        </div>
    );
}

export default FiltersBar;