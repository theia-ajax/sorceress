using UnityEngine;

public class GameController : MonoBehaviour
{
    public MechMaterialController m_MaterialTest;
    public PlayerIndex m_PlayerIndex = PlayerIndex.Player1;
    private PlayerIndex m_LastPlayerIndex = PlayerIndex.Invalid;

    void Start()
    {
        UpdatePlayerIndex(m_PlayerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerIndex(m_PlayerIndex);
    }

    void UpdatePlayerIndex(PlayerIndex playerIndex)
    {
        if (m_LastPlayerIndex != playerIndex)
        {
            if (m_MaterialTest)
            {
                m_MaterialTest.ApplyPlayerColors(GetComponent<PlayerColors>(), playerIndex);
            }
            m_LastPlayerIndex = playerIndex;
        }
    }
}
