
    CREATE PROCEDURE [dbo].[TempRemoveStateItem]
        @id     tSessionId,
        @lockCookie int
    AS
        DELETE ASPStateTempSessions
        WHERE SessionId = @id AND LockCookie = @lockCookie
        RETURN 0