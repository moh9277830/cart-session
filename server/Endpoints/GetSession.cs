namespace Server.Endpoints;

public class SessionEnpoint
{
    public static IResult GetSession(ISessionManager sessionManager)
    {
        return Results.Ok("Session");
    }
}