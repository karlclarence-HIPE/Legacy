CREATE TABLE Vehicles (
    vehicle_id SERIAL PRIMARY KEY,
    customer_id INT REFERENCES Customers(customer_id),
    plate_number VARCHAR(20) UNIQUE NOT NULL,
    brand VARCHAR(50),
    model VARCHAR(50),
    year INT,
    engine_number VARCHAR(100),
    image_url TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);