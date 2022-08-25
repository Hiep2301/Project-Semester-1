drop database if exists bookstore;
create database bookstore;
use bookstore;

create table if not exists customer(
	customer_id int auto_increment primary key,
    customer_name varchar(45) not null,
    customer_phone varchar (10) not null
);

create table if not exists staff(
	staff_id int primary key auto_increment,
    staff_name varchar(45) not null,
    staff_phone varchar(10) not null unique,
    staff_address  varchar(100),
    staff_role int not null default 1, -- 1. staff
    staff_username varchar(45) not null unique,
    staff_password varchar(45) not null
);

insert into staff(staff_name, staff_username, staff_password, staff_phone, staff_address, staff_role)
values ('Ngô Trấn Hiệp', 'hiep2301', '5129d430ce2687db44d7cddcbee553c8', '0942878948', 'Hà Nội', 1),
('Đặng Trần Phong', 'phong123', 'd54b0496bfa0446d31c45884ff19a41a', '0347341761', 'Phú Thọ', 1);

create table if not exists category(
	category_id int auto_increment primary key,
    category_name varchar(45) not null
);
insert into category(category_name)
values ('Văn Học'),
('Kinh Tế'),
('Tâm Lý - Kỹ Năng Sống'),
('Nuôi Dạy Con'),
('Sách Thiếu Nhi'),
('Tiểu Sử - Hồi Ký'),
('Sách Giáo Khoa - Tham Khảo'),
('Sách Học Ngoại Ngữ');


create table if not exists book(
	book_id int auto_increment primary key,
    category_id int not null,
    book_name varchar(45) not null,
    author_name varchar(45) not null,
	book_price decimal not null,
    book_description varchar(500) not null,
    book_quantity int not null,
    foreign key (category_id) references category(category_id)
);

create table orders(
	order_id int auto_increment primary key,
    staff_id int not null,
    customer_id int not null,
    order_date datetime default current_timestamp() not null,
    order_status int not null, -- 1: Create New Order
    foreign key (staff_id) references staff(staff_id),
    foreign key (customer_id) references customer(customer_id)
);

create table order_details(
	order_id int not null,
    book_id int not null,
    quantity int not null,
    unit_price decimal not null,	
    primary key (order_id, book_id),
    foreign key (order_id) references orders(order_id),
    foreign key (book_id) references book(book_id)
);