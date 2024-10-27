CREATE TABLE AcademicTerms (
    TermId INT PRIMARY KEY IDENTITY(1,1),
    TermName NVARCHAR(50) NOT NULL, -- e.g., "Fall 2024" or "Spring 2025"
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1, -- Indicates if this is an active term
	[CreatedOn] DATETIME DEFAULT GETDATE(),
	[UpdatedOn] DATETIME NULL
);