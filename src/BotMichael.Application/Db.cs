namespace BotMichael.Application;

public static class Db
{
    private static readonly Dictionary<long, BaseState> _db = new();
    public static BaseState GetState(long userId)
    {
        return _db.TryGetValue(userId, out var result) 
            ? result
            : new InitialState();
    }

    public static void SaveState(long userId, BaseState newBaseState)
    {
        if (_db.TryGetValue(userId, out var state))
        {
            _db[userId] = newBaseState;
            return;
        }
        _db.Add(userId, newBaseState);
    }
}