import React from "react";

const FiltersBar = ({ onFilterChange, onSubmitFilters, onResetFilters, filters }) => {
    return (
        <div className="filters">
            <div className="filter-input-group">
                <input
                    type="text"
                    placeholder="Location"
                    value={filters.location}
                    onChange={(e) => onFilterChange('location', e.target.value)}
                    className="filter-input location-input"
                />
            </div>
            <div className="filter-input-group">
                <input
                    type="number"
                    placeholder="Min Price"
                    value={filters.minPrice}
                    onChange={(e) => onFilterChange('minPrice', e.target.value)}
                    className="filter-input"
                />
                <input
                    type="number"
                    placeholder="Max Price"
                    value={filters.maxPrice}
                    onChange={(e) => onFilterChange('maxPrice', e.target.value)}
                    className="filter-input"
                />
            </div>
            <div className="filter-input-group">
                <input
                    type="number"
                    placeholder="Min Size"
                    value={filters.minSize}
                    onChange={(e) => onFilterChange('minSize', e.target.value)}
                    className="filter-input"
                />
                <input
                    type="number"
                    placeholder="Max Size"
                    value={filters.maxSize}
                    onChange={(e) => onFilterChange('maxSize', e.target.value)}
                    className="filter-input"
                />
            </div>
            <div className="filter-input-group">
                <input
                    type="number"
                    placeholder="Min Rooms"
                    value={filters.minRooms}
                    onChange={(e) => onFilterChange('minRooms', e.target.value)}
                    className="filter-input"
                />
                <input
                    type="number"
                    placeholder="Max Rooms"
                    value={filters.maxRooms}
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