CREATE TABLE Payments (
    payment_id SERIAL PRIMARY KEY,
    service_id INT REFERENCES Service_Records(service_id),
    amount NUMERIC(10,2) NOT NULL,
    payment_method VARCHAR(50),
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);