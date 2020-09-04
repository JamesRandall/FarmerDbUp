create table [dbo].[Products] (
    Id          uniqueidentifier not null constraint PK_Products primary key NONCLUSTERED,
    Name        nvarchar (128) not null
)
go 