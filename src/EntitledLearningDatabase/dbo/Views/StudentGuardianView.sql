CREATE VIEW [dbo].[StudentGuardiansView]
AS
SELECT
    s.Id AS StudentId,
    s.FirstName AS StudentFirstName,
    s.LastName AS StudentLastName,
    g.Id AS GuardianId,
    g.FirstName AS GuardianFirstName,
    g.LastName AS GuardianLastName,
    r.IsEmergencyContact,
    r.IsAuthorizedPickup
FROM
    Student s
LEFT JOIN
    GuardianStudentRelationship r ON s.Id = r.StudentId
LEFT JOIN
    Guardian g ON r.GuardianId = g.Id;
