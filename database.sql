use master
go
drop database if exists reizzz_tracking_v1
go
create database reizzz_tracking_v1
go
use reizzz_tracking_v1
go
-- create table
CREATE TABLE users (
    Id bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Username nvarchar(50)  NOT NULL UNIQUE,
    Password nvarchar(250) ,
    Name nvarchar(100) NOT NULL,
    Email nvarchar(100),
    PhoneNumber nvarchar(10) NOT NULL,
    Gender tinyint NOT NULL,
    Birthday date NOT NULL,
    Bio nvarchar(250),
    CreatedBy bigint,
    CreatedDate datetime,
    UpdatedBy bigint,
    UpdatedDate datetime
)
CREATE TABLE roles (
    Id bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(50)  NOT NULL UNIQUE
)
CREATE TABLE user_roles (
    Id bigint NOT NULL IDENTITY(1,1),
    UserId bigint NOT NULL,
    RoleId bigint NOT NULL,
	PRIMARY KEY(Id, UserId, RoleId)
)
CREATE TABLE category_types (
    Id bigint IDENTITY(1,1) PRIMARY KEY,
    Type nvarchar(50)
)
CREATE TABLE user_tasks (
    Id bigint IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100),
    AppliedFor bigint,
    Description nvarchar(250),
    Remark nvarchar(250),
    CreatedAt datetime
)
CREATE TABLE routine_collections (
    Id bigint IDENTITY(1,1) PRIMARY KEY,
    CreatedBy bigint,
    Name nvarchar(100),
    IsPublic bit
)
CREATE TABLE [routines] (
    Id bigint IDENTITY(1,1) PRIMARY KEY,
    StartTime nvarchar(50),
    Name nvarchar(100),
    IsPublic bit,
    UsedBy bigint,
    CategoryType bigint
)
CREATE TABLE todo_schedule (
    Id bigint IDENTITY(1,1) PRIMARY KEY,
    StartAt datetime,
    EndAt datetime,
    ToDoId bigint,
    AppliedFor bigint,
    IsDone bit,
    EstimatedTime int,
    ActualTime int,
    TimeUnitId bigint,
    CategoryType bigint
)
CREATE TABLE time_units (
    Id bigint IDENTITY(1,1) PRIMARY KEY,
    Name datetime
)
CREATE TABLE time_exchanges (
    Id bigint IDENTITY(1,1) PRIMARY KEY,
    FromUnitId bigint,
    ToUnitId bigint,
    Multiplier decimal(5,2)
)
go

-- foreign key
ALTER TABLE user_roles
ADD FOREIGN KEY (UserId) REFERENCES users(Id);

ALTER TABLE user_roles
ADD FOREIGN KEY (RoleId) REFERENCES roles(Id);

ALTER TABLE user_tasks
ADD FOREIGN KEY (AppliedFor) REFERENCES users(Id);

ALTER TABLE routine_collections
ADD FOREIGN KEY (CreatedBy) REFERENCES users(Id);

ALTER TABLE user_roles
ADD FOREIGN KEY (UserId) REFERENCES users(Id);

ALTER TABLE [routines]
ADD FOREIGN KEY (UsedBy) REFERENCES users(Id);

ALTER TABLE [routines]
ADD FOREIGN KEY (CategoryType) REFERENCES category_types(Id);

ALTER TABLE [todo_schedule]
ADD FOREIGN KEY (AppliedFor) REFERENCES users(Id);

ALTER TABLE [todo_schedule]
ADD FOREIGN KEY (TimeUnitId) REFERENCES time_units(Id);

ALTER TABLE [todo_schedule]
ADD FOREIGN KEY (CategoryType) REFERENCES category_types(Id);

ALTER TABLE [time_exchanges]
ADD FOREIGN KEY (FromUnitId) REFERENCES time_units(Id);

ALTER TABLE [time_exchanges]
ADD FOREIGN KEY (ToUnitId) REFERENCES time_units(Id);