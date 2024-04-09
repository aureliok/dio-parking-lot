import React from "react";

function VehiclesPage(): JSX.Element {
    return (
      <div className="container page2">
        <h2>Vehicles Page</h2>
        <div className="row">
            <button className="col-4">Add</button>
            <button className="col-4">Update</button>
            <button className="col-4">Remove</button>
          </div>
      </div>
    );
  }

export default VehiclesPage;