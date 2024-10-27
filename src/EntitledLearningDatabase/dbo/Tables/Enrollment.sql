CREATE TABLE Enrollment (
    EnrollmentId INT PRIMARY KEY IDENTITY(1,1),
    StudentId VARCHAR(10) NOT NULL,
    TermId INT NOT NULL, -- New column to associate enrollment with a term
    EnrollmentDate DATETIME DEFAULT GETDATE(),
    EnrollmentStatus NVARCHAR(50) CHECK (EnrollmentStatus IN ('Pending', 'Active', 'Completed', 'Dropped')),
    Notes NVARCHAR(1000),
    [CreatedOn] DATETIME DEFAULT GETDATE(),
	[UpdatedOn] DATETIME NULL
    FOREIGN KEY (StudentId) REFERENCES Student(Id),
    FOREIGN KEY (TermId) REFERENCES AcademicTerms(TermId)
);
