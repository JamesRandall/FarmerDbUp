alter table [dbo].[Products] add Quantity int null
go
update [dbo].[Products] set Quantity = 0
go
alter table [dbo].[Products] alter column Quantity int not null
go
