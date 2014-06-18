CREATE TABLE [dbo].[User] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Username]  NVARCHAR (MAX) NOT NULL,
    [Firstname] NVARCHAR (MAX) NOT NULL,
    [Lastname]  NVARCHAR (MAX) NOT NULL,
    [IsEnable]  BIT            NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

