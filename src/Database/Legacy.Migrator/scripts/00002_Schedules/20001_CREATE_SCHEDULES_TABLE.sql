CREATE TABLE Schedules (
    schedule_id SERIAL PRIMARY KEY,
    vehicle_id INT REFERENCES Vehicles(vehicle_id),
    scheduled_date DATE NOT NULL,
    service_type_id INT REFERENCES Service_Types(service_type_id),
    status VARCHAR(50),
    notes TEXT
);