import React from "react";



function ClientsPage(): JSX.Element {
    return (
      <div className="container page2">
        <h2>Clients Page</h2>
        <div className="row">
            <button className="col-3">Add</button>
            <button className="col-3">Update</button>
            <button className="col-3">Remove</button>
            <button className="col-3">Get Vehicles</button>
          </div>
      </div>
    );
  }


export default ClientsPage;