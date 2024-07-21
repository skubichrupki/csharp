create table todo.task
(
    task_id int primary key identity,
    task_desc varchar(255) not null
)

create table todo.status
(
    status_id int primary key identity,
    status_desc varchar(255) not null
)

alter table todo.task
add status_id int foreign key (status_id) references todo.status (status_id)
go