CREATE TABLE Enrollment (
    EnrollmentId INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    TermId INT NOT NULL, -- New column to associate enrollment with a term
    EnrollmentDate DATE DEFAULT GETDATE(),
    EnrollmentStatus NVARCHAR(50) CHECK (EnrollmentStatus IN ('Pending', 'Active', 'Completed', 'Dropped')),
    Notes NVARCHAR(1000),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId),
    FOREIGN KEY (TermId) REFERENCES AcademicTerms(TermId)
);