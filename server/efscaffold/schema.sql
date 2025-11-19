drop schema if exists birdy cascade;
create schema birdy;

create table birdy.players(
                              id text not null primary key,
                              name text not null,
                              email text not null
);
