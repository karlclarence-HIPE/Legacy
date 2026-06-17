CREATE TABLE Service_Types (
    service_type_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    base_price NUMERIC(10,2) DEFAULT 0
);