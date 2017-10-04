CREATE VIEW [dbo].[UserRole_V]
AS
SELECT        U.UserId, U.UserName, U.FirstName, U.LastName,
    substring(
        (
            Select ','+R.RoleName AS [text()]
            From dbo.webpages_UsersInRoles UR INNER JOIN
                         dbo.webpages_Roles R ON UR.RoleId = R.RoleId
            Where UR.UserId = U.UserId 
            For XML PATH ('')
        ), 2, 1000) [RoleNames]
FROM            dbo.UserProfile U