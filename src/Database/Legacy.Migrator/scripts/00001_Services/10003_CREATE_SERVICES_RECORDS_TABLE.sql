CREATE TABLE Service_Records (
    service_id SERIAL PRIMARY KEY,
    vehicle_id INT REFERENCES Vehicles(vehicle_id),
    assigned_mechanic_id INT REFERENCES Users(user_id),
    service_date TIMESTAMP NOT NULL,
    status_id INT REFERENCES Service_Status(status_id),
    total_cost NUMERIC(10,2) DEFAULT 0,
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);