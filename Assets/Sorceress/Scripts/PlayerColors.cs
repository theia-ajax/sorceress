using UnityEngine;

public enum PlayerColorSlot
{
    Primary,
    Secondary,
    EmissivePrimary,
    EmissiveSecondary,
}

public interface IPlayerColors
{
    Color GetPlayerColor(PlayerIndex playerIndex, PlayerColorSlot colorSlot = PlayerColorSlot.Primary);
    Color DefaultPlayerColor(PlayerColorSlot colorSlot = PlayerColorSlot.Primary);
    int ColorsVersionId { get; }
}

public class PlayerColors : MonoBehaviour, IPlayerColors
{
    [System.Serializable]
    private struct PlayerColorsEntry
    {
        public Color Primary;
        public Color Secondary;
        [ColorUsage(false, true)]
        public Color EmissivePrimary;
        [ColorUsage(false, true)]
        public Color EmissiveSecondary;
        public Color GetColor(PlayerColorSlot slot)
        {
            switch (slot)
            {
                default:
                case PlayerColorSlot.Primary: return Primary;
                case PlayerColorSlot.Secondary: return Secondary;
                case PlayerColorSlot.EmissivePrimary: return EmissivePrimary;
                case PlayerColorSlot.EmissiveSecondary: return EmissiveSecondary;
            }
        }

        public bool Equals(PlayerColorsEntry other)
        {
            return Primary == other.Primary &&
                Secondary == other.Secondary &&
                EmissivePrimary == other.EmissivePrimary &&
                EmissiveSecondary == other.EmissiveSecondary;
        }
    }

    [SerializeField] private PlayerColorsEntry[] m_PlayerColorEntries = new PlayerColorsEntry[(int)PlayerIndex.PlayersLast - (int)PlayerIndex.PlayersFirst];

    private PlayerColorsEntry[] m_LastPlayerColorEntries;
    private int m_ColorsVersionId = 0;

    public int ColorsVersionId => m_ColorsVersionId;

    void LateUpdate()
    {
        if (CheckIfColorsDirty())
        {
            m_LastPlayerColorEntries = new PlayerColorsEntry[m_PlayerColorEntries.Length];
            int index = 0;
            foreach (var entry in m_PlayerColorEntries)
            {
                m_LastPlayerColorEntries[index++] = entry;
            }
            m_ColorsVersionId++;
        }
        
    }

    private bool CheckIfColorsDirty()
    {
        if (m_LastPlayerColorEntries == null)
            return true;

        if (m_LastPlayerColorEntries.Length != m_PlayerColorEntries.Length)
            return true;

        for (int index = 0; index < m_PlayerColorEntries.Length; index++)
        {
            if (!m_PlayerColorEntries[index].Equals(m_LastPlayerColorEntries[index]))
                return true;
        }

        return false;
    }

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
            case PlayerColorSlot.EmissivePrimary: return Color.white;
        }
    }
}