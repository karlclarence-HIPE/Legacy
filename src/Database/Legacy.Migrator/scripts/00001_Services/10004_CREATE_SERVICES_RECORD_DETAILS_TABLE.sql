CREATE TABLE Service_Record_Details (
    detail_id SERIAL PRIMARY KEY,
    service_id INT REFERENCES Service_Records(service_id),
    service_type_id INT REFERENCES Service_Types(service_type_id),
    quantity INT DEFAULT 1,
    price NUMERIC(10,2) NOT NULL
);