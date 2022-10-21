namespace BotMichael.UnitTests;

public class Test
{
    [Fact]
    public void Start()
    {
        const long userId = 9;
        var startMessage = new StartRequest(userId);

        var reply = startMessage.CreateReply();
        var expected = new GetEmailReply(new TextContent("Отправь почту", userId));

        Assert.Equal(expected, reply);
    }
    
    [Theory]
    [MemberData(nameof(SetEmailData))]
    public void SetEmail(string email, long userId, ReplyMessage expected)
    {
        var startMessage = new SetEmailRequest(email, userId);

        var reply = startMessage.CreateReply();

        Assert.Equal(expected, reply);
    }
    
    public static IEnumerable<object[]> SetEmailData
    {
        get
        {
            const long userId = 1;
            return new List<object[]>
            {
                new object[]
                {
                    "hello@privet.ru",
                    userId,
                    new GetPasswordReply(new TextContent("Код отправил на почту. Укажи его сюда, как придет", userId))
                },
                new object[]
                {
                    "privet@hello.ru",
                    userId,
                    new ErrorReply(new TextContent("Почта неправильная", userId))
                },
            };
        }
    }
    
    [Theory]
    [MemberData(nameof(SetPasswordData))]
    public void SetPassword(string password, long userId, ReplyMessage expected)
    {
        var startMessage = new SetPasswordRequest(password, userId);

        var reply = startMessage.CreateReply();

        Assert.Equal(expected, reply);
    }
    
    public static IEnumerable<object[]> SetPasswordData
    {
        get
        {
            const long userId = 1;
            return new List<object[]>
            {
                new object[]
                {
                    "123",
                    userId,
                    new ReadyReply(new TextContent("Все ок", userId))
                },
                new object[]
                {
                    "321",
                    userId,
                    new ErrorReply(new TextContent("Пароль неправильный", userId))
                },
            };
        }
    }
}