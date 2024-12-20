CREATE TABLE Products
(
    ProductId serial primary key,
    Name      varchar(100),
    Price     decimal CHECK (Price > 0),
    Stock     int
);

CREATE TABLE Orders
(
    OrderId    serial PRIMARY key,
    ProductId  int REFERENCES Products (ProductId),
    Quantity   int,
    TotalPrice decimal,
    OrderDate  date DEFAULT CURRENT_DATE
);