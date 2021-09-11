drop database unitydb;

create database unitydb;
use unitydb;

create table user (id int,name varchar(32),castumdata varchar(255),primary key(id));

insert into user (id,name,castumdata) values (1,"hoge","userNo1");
insert into user (id,name,castumdata) values (2,"huga","userNo2");
insert into user (id,name,castumdata) values (3,"hogehuga","userNo3");


