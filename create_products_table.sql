-- Create Products table
CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY(1,1),
    [Name] nvarchar(200) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [Price] decimal(18,2) NOT NULL,
    [Stock] int NOT NULL,
    [CreatedAt] datetime NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);

-- Optional: Insert some sample data for testing
INSERT INTO [Products] ([Name], [Description], [Price], [Stock], [CreatedAt]) VALUES
('Sample Product 1', 'This is a sample product for testing', 29.99, 100, GETDATE()),
('Sample Product 2', 'Another sample product', 49.99, 50, GETDATE()),
('Sample Product 3', 'Third sample product', 19.99, 75, GETDATE());
