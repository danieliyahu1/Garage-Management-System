**C# Vehicle Garage Management System**

This project implements a multi-purpose vehicle garage management system in C#. It showcases object-oriented programming principles, inheritance, polymorphism, data structures, and external assemblies (.dll). 

**Project Objectives:**

- Integrate classes, inheritance, and polymorphism for robust object modeling.
- Utilize arrays, collections, and data structures for efficient data management.
- Employ enums to represent vehicle types, fuel types, and statuses.
- Develop and leverage a separate assembly (.dll) for the logical layer.
- Manage multiple projects within a solution for better organization.
- Implement exception handling for error resilience.

**System Functionality:**

The system manages a virtual garage that can handle five vehicle types:

- **Fuel-Based Motorcycle:** 2 tires, max air pressure 30 psi, Octane 95 fuel, 7 L fuel tank.
- **Electric Motorcycle:** 2 tires, max air pressure 30 psi, max battery life 1.2 hours.
- **Fuel-Based Car:** 4 tires, max air pressure 32 psi, Octane 96 fuel, 60 L fuel tank.
- **Electric Car:** 4 tires, max air pressure 32 psi, max battery life 2.1 hours.
- **Fuel-Based Truck:** 16 tires, max air pressure 28 psi, Soler fuel, 120 L fuel tank.

**Vehicle Properties:**

- **Common Properties:**
    - Model Name (string)
    - License Number (string)
    - Remaining Energy Percentage (fuel/battery) (float)
    - Wheels (list) with individual wheel properties:
        - Manufacturer Name (string)
        - Current Air Pressure (float)
        - Max Air Pressure (float)
        - `InflateAction` method (adds air pressure up to the maximum)
- **Motorcycle-Specific Properties:**
    - License Type (enum: A, A1, AA, B)
    - Engine Volume (int)
- **Car-Specific Properties:**
    - Color (enum: Red, Blue, Black, Gray)
    - Number of Doors (int: 2, 3, 4, or 5)
- **Truck-Specific Properties:**
    - Contains Dangerous Materials? (bool)
    - Cargo Volume (float)
- **Fuel-Based Vehicle Properties:**
    - Fuel Type (enum: Soler, Octane 95, Octane 96, Octane 98)
    - Current Fuel Amount (Liters) (float)
    - Max Fuel Capacity (Liters) (float)
    - `Refueling` method (adds fuel of the correct type, respecting tank capacity)
- **Electric-Based Vehicle Properties:**
    - Remaining Engine Operation Time (Hours) (float)
    - Max Engine Operation Time (Hours) (float)
    - `Recharge` method (increases battery charge up to the maximum)
- **Garage Management Properties:**
    - Owner Name (string)
    - Owner Phone Number (string)
    - Vehicle Status (enum: In Repair, Repaired, Paid For) (new vehicles are initially "In Repair")

**System Features:**

The system offers the following functionalities:

1. **Add Vehicle:** Users can select a vehicle type and enter the license number. If the vehicle already exists (based on license number), its status is updated to "In Repair." Otherwise, a new object of the chosen type is created, prompting the user to enter vehicle details.
2. **List Vehicles:** Displays a list of license plates in the garage, with an option to filter by vehicle status.
3. **Change Vehicle Status:** Users can input a license number and a new desired status to update a vehicle's status.
4. **Inflate Tires (Maximum):** Users can input a license number to automatically inflate all vehicle tires to their maximum pressure.
5. **Refuel (Fuel-Based Vehicles):** Users can input a license number, fuel type, and amount to refuel a vehicle. The
