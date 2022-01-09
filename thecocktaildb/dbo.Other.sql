CREATE TABLE [dbo].[Other] (
    [Id]         INT          NOT NULL,
    [Name]       VARCHAR (50) NOT NULL,
    [Amount]     REAL         DEFAULT ((0)) NULL,
    [Shelf life] TIME (7)     NULL,
    [Price]      MONEY        DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

