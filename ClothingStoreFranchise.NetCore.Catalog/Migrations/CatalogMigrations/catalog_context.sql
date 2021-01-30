IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Offers] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Discount] decimal(18,2) NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [CatalogProductId] bigint NULL,
    [CategoryId] bigint NULL,
    CONSTRAINT [PK_Offers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Categories] (
    [Id] bigint NOT NULL IDENTITY(0, 1),
    [Name] nvarchar(max) NULL,
    [CategoryBelongingId] bigint NULL,
    [ClothingSizeType] int NULL,
    [CurrentOfferId] bigint NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Categories_Categories_CategoryBelongingId] FOREIGN KEY ([CategoryBelongingId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Categories_Offers_CurrentOfferId] FOREIGN KEY ([CurrentOfferId]) REFERENCES [Offers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [CatalogProducts] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [PictureUrl] nvarchar(max) NULL,
    [CurrentOfferId] bigint NULL,
    [SubcategoryId] bigint NOT NULL,
    CONSTRAINT [PK_CatalogProducts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CatalogProducts_Offers_CurrentOfferId] FOREIGN KEY ([CurrentOfferId]) REFERENCES [Offers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CatalogProducts_Categories_SubcategoryId] FOREIGN KEY ([SubcategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_CatalogProducts_CurrentOfferId] ON [CatalogProducts] ([CurrentOfferId]);

GO

CREATE INDEX [IX_CatalogProducts_SubcategoryId] ON [CatalogProducts] ([SubcategoryId]);

GO

CREATE INDEX [IX_Categories_CategoryBelongingId] ON [Categories] ([CategoryBelongingId]);

GO

CREATE INDEX [IX_Categories_CurrentOfferId] ON [Categories] ([CurrentOfferId]);

GO

CREATE INDEX [IX_Offers_CatalogProductId] ON [Offers] ([CatalogProductId]);

GO

CREATE INDEX [IX_Offers_CategoryId] ON [Offers] ([CategoryId]);

GO

ALTER TABLE [Offers] ADD CONSTRAINT [FK_Offers_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [Offers] ADD CONSTRAINT [FK_Offers_CatalogProducts_CatalogProductId] FOREIGN KEY ([CatalogProductId]) REFERENCES [CatalogProducts] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201104174942_Initial', N'3.1.10');

GO

