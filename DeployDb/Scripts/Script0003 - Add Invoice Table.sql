create table [dbo].[Invoices] (
    Id          uniqueidentifier not null constraint PK_Invoices primary key NONCLUSTERED,
    CreatedAt   datetime2 not null
)
