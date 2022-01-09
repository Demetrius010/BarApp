CREATE TABLE [dbo].[Soda] (
    [Id]         INT          NOT NULL,
    [Name]       VARCHAR (50) NOT NULL,
    [Capacity]   REAL         DEFAULT ((0)) NULL,
    [Shelf life] TIME (7)     NULL,
    [Price]      MONEY        DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

