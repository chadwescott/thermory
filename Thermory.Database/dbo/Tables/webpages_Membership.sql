CREATE TABLE [dbo].[webpages_Membership] (
    [UserId]                                  INT            NOT NULL,
    [CreateDate]                              DATETIME       NULL,
    [ConfirmationToken]                       NVARCHAR (128) NULL,
    [IsConfirmed]                             BIT            CONSTRAINT [DF__webpages___IsCon__467D75B8] DEFAULT ((0)) NULL,
    [LastPasswordFailureDate]                 DATETIME       NULL,
    [PasswordFailuresSinceLastSuccess]        INT            CONSTRAINT [DF__webpages___Passw__477199F1] DEFAULT ((0)) NOT NULL,
    [Password]                                NVARCHAR (128) NOT NULL,
    [PasswordChangedDate]                     DATETIME       NULL,
    [PasswordSalt]                            NVARCHAR (128) NOT NULL,
    [PasswordVerificationToken]               NVARCHAR (128) NULL,
    [PasswordVerificationTokenExpirationDate] DATETIME       NULL,
    CONSTRAINT [PK__webpages__1788CC4C3D7975D5] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

