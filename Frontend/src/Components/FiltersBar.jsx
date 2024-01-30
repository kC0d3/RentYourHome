import React from "react";

function FiltersBar({ onFilterSelect }) {
    return (
        <div>
            <button onClick={() => onFilterSelect('filter1')}>Specific Filter 1</button>
            <button onClick={() => onFilterSelect('filter2')}>Specific Filter 2</button>
            <button onClick={() => onFilterSelect('filter3')}>Specific Filter 3</button>
        </div>
    );
}

export default FiltersBar;