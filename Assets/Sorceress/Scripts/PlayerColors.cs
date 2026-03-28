using UnityEngine;

public enum PlayerColorSlot
{
    Primary,
    Secondary,
}

public interface IPlayerColors
{
    Color GetPlayerColor(PlayerIndex playerIndex, PlayerColorSlot colorSlot = PlayerColorSlot.Primary);
    Color DefaultPlayerColor(PlayerColorSlot colorSlot = PlayerColorSlot.Primary);
}



public class PlayerColors : MonoBehaviour, IPlayerColors
{
    [System.Serializable]
    private struct PlayerColorsEntry
    {
        public Color PrimaryColor;
        public Color SecondaryColor;
        public Color GetColor(PlayerColorSlot slot)
        {
            switch (slot)
            {
                default:
                case PlayerColorSlot.Primary: return PrimaryColor;
                case PlayerColorSlot.Secondary: return SecondaryColor;
            }
        }
    }

    [SerializeField] private PlayerColorsEntry[] m_PlayerColorEntries = new PlayerColorsEntry[(int)PlayerIndex.PlayersLast - (int)PlayerIndex.PlayersFirst];


    public Color GetPlayerColor(PlayerIndex playerIndex, PlayerColorSlot colorSlot = PlayerColorSlot.Primary)
    {
        int index = PlayerIndexUtil.ToIndex(playerIndex);
        if (index < 0 || index >= m_PlayerColorEntries.Length)
        {
            return DefaultPlayerColor(colorSlot);
        }
        return m_PlayerColorEntries[index].GetColor(colorSlot);
    }

    public Color DefaultPlayerColor(PlayerColorSlot colorSlot)
    {
        switch (colorSlot)
        {
            default:
            case PlayerColorSlot.Primary: return Color.white;
            case PlayerColorSlot.Secondary: return Color.gray;
        }
    }
}