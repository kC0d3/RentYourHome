import React from "react";

function FiltersBar({ onFilterSelect }) {
    return (
        <div className="filters">
            <button onClick={() => onFilterSelect('filter1')} className="filter-button">Specific Filter 1</button>
            <button onClick={() => onFilterSelect('filter2')} className="filter-button">Specific Filter 2</button>
            <button onClick={() => onFilterSelect('filter3')} className="filter-button">Specific Filter 3</button>
        </div>
    );
}

export default FiltersBar;