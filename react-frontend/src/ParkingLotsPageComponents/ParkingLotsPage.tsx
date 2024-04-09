import { useState } from "react";
import AddFormFields from "./AddFormFields";
import UpdateFormFields from "./UpdateFormFields";
import RemoveFormFields from "./RemoveFormFields";
import GetVehicleFormFields from "./GetVehicleFormFields";


function ParkingLotsPage(): JSX.Element {
  const [selectedOption, setSelectedOption] = useState<string | null>(null);

  const handleButtonClick = (option: string): void => {
    setSelectedOption(option);
  }

    return (
      <div className="container page2">
        <h2>Parking Lots Page</h2>
          <div className="row">
            <button className="col-3" onClick={() => handleButtonClick("Add")}>Add</button>
            <button className="col-3" onClick={() => handleButtonClick("Update")}>Update</button>
            <button className="col-3" onClick={() => handleButtonClick("Remove")}>Remove</button>
            <button className="col-3" onClick={() => handleButtonClick("GetVehicles")}>Get Vehicles</button>
          </div>

          {selectedOption && (
            <div className="row">
              {selectedOption === "Add" && <AddFormFields />}
              {selectedOption === "Update" && <UpdateFormFields />}
              {selectedOption === "Remove" && <RemoveFormFields />}
              {selectedOption === "GetVehicles" && <GetVehicleFormFields />}
            </div>
          )}
      </div>
    );
  }

export default ParkingLotsPage;