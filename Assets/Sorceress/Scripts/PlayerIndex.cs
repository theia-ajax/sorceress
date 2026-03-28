public enum PlayerIndex
{
    Invalid,
    Player1,
    Player2,
    Player3,
    Player4,

    PlayersLast,

    PlayersFirst = Player1,
    PlayersCount = PlayersLast - PlayersFirst,
}

public static class PlayerIndexUtil
{
    public static int ToIndex(PlayerIndex playerIndex)
    {
        int index = playerIndex - PlayerIndex.PlayersFirst;
        return IsValid(playerIndex) ? index : -1;
    }

    public static bool IsValid(PlayerIndex playerIndex)
    {
        int index = playerIndex - PlayerIndex.PlayersFirst;
        return index >= 0 && index < (int)PlayerIndex.PlayersCount;
    }
}