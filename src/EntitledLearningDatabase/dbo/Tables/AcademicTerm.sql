CREATE TABLE AcademicTerms (
    TermId INT PRIMARY KEY IDENTITY(1,1),
    TermName NVARCHAR(50) NOT NULL, -- e.g., "Fall 2024" or "Spring 2025"
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1, -- Indicates if this is an active term
    DateCreated DATETIME2 DEFAULT GETDATE()
);