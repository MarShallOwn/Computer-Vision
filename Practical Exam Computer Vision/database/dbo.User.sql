CREATE TABLE [dbo].[User] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [username] NVARCHAR (50)  NOT NULL,
    [password] NVARCHAR (200) NOT NULL,
    [admin] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([username] ASC),
    UNIQUE NONCLUSTERED ([password] ASC)
);

