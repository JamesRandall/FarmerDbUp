alter table [dbo].[Invoices] add Total int null
go
update [dbo].[Invoices] set Total = 0
go
alter table [dbo].[Invoices] alter column Total int not null
go

