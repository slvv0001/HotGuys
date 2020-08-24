CREATE TABLE [dbo].[Comments] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Rating]      REAL     NOT NULL,
    [Comment] NVARCHAR (MAX) NULL,
    [UserId]      NVARCHAR (128) NULL,
    [HotChoiceId] INT NULL,
    CONSTRAINT [PK_dbo.Comments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Comments_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.Comments_dbo.Hotchoices_Id] FOREIGN KEY ([HotChoiceId]) REFERENCES [dbo].[HotChoiceViewModels] ([Id])

);


GO

