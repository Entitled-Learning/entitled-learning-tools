CREATE TABLE [dbo].[GuardianStudentRelationship]
(
  [GuardianId] VARCHAR(10) NOT NULL,
  [StudentId] VARCHAR(10) NOT NULL,
  [Relationship] [nvarchar](50) NULL,
  [IsEmergencyContact] [tinyint] NOT NULL,
  [IsAuthorizedPickup] [tinyint] NOT NULL,
  [CreatedOn] DATETIME DEFAULT GETDATE(),
	[UpdatedOn] DATETIME NULL,
  PRIMARY KEY ([GuardianId], [StudentId]),
  FOREIGN KEY ([GuardianId]) REFERENCES [Guardian]([Id]),
  FOREIGN KEY ([StudentId]) REFERENCES [Student]([Id])
);