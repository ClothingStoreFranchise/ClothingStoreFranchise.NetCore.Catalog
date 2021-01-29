IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [IntegrationEventLog] (
    [EventId] uniqueidentifier NOT NULL,
    [EventTypeName] nvarchar(max) NOT NULL,
    [State] int NOT NULL,
    [TimesSent] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [TransactionId] nvarchar(max) NULL,
    CONSTRAINT [PK_IntegrationEventLog] PRIMARY KEY ([EventId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201003165707_Initial', N'3.1.10');

GO

