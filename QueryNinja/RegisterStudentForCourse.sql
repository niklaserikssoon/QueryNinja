CREATE PROCEDURE RegisterStudentForCourse
    @StudentId INT,
    @CourseId INT
AS
BEGIN
    SET NOCOUNT ON;

    
    IF NOT EXISTS (SELECT 1 FROM Students WHERE StudentID = @StudentId)
    BEGIN
        SELECT ResultMessage = 'Error: Student not found.'
        RETURN
    END

    
    IF NOT EXISTS (SELECT 1 FROM Courses WHERE CourseId = @CourseId)
    BEGIN
        SELECT ResultMessage = 'Error: Course not found.'
        RETURN
    END

   
    IF EXISTS (SELECT 1 FROM Registrations WHERE FkStudentId = @StudentId AND FkCourseId = @CourseId)
    BEGIN
        SELECT ResultMessage = 'Error: Student is already registered for this course.'
        RETURN
    END

   
    INSERT INTO Registrations (FkStudentId, FkCourseId, RegistrationDate)
    VALUES (@StudentId, @CourseId, GETDATE());

    SELECT ResultMessage = 'Success: Student registered successfully.'
END
GO