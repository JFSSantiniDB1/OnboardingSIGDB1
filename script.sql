IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CARGO] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(250) NOT NULL,
    CONSTRAINT [PK_CARGO] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [EMPRESA] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(150) NOT NULL,
    [Cnpj] nvarchar(14) NOT NULL,
    [DataFundacao] datetime2 NULL,
    CONSTRAINT [PK_EMPRESA] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FUNCIONARIO] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(150) NOT NULL,
    [Cpf] nvarchar(11) NOT NULL,
    [DataContratacao] datetime2 NULL,
    [IdEmpresa] int NULL,
    CONSTRAINT [PK_FUNCIONARIO] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FUNCIONARIO_EMPRESA_IdEmpresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [EMPRESA] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [FUNCIONARIOXCARGO] (
    [Id] int NOT NULL IDENTITY,
    [IdFuncionario] int NOT NULL,
    [IdCargo] int NOT NULL,
    [DataVinculo] datetime2 NOT NULL,
    CONSTRAINT [PK_FUNCIONARIOXCARGO] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FUNCIONARIOXCARGO_CARGO_IdCargo] FOREIGN KEY ([IdCargo]) REFERENCES [CARGO] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FUNCIONARIOXCARGO_FUNCIONARIO_IdFuncionario] FOREIGN KEY ([IdFuncionario]) REFERENCES [FUNCIONARIO] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_FUNCIONARIO_IdEmpresa] ON [FUNCIONARIO] ([IdEmpresa]);
GO

CREATE INDEX [IX_FUNCIONARIOXCARGO_IdCargo] ON [FUNCIONARIOXCARGO] ([IdCargo]);
GO

CREATE INDEX [IX_FUNCIONARIOXCARGO_IdFuncionario] ON [FUNCIONARIOXCARGO] ([IdFuncionario]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825215629_Initial', N'5.0.17');
GO

COMMIT;
GO

