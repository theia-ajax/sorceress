using UnityEngine;

public class PlayerMechDisplay : MonoBehaviour
{
    public GameObject m_MechPrefab;
    public PlayerIndex m_PlayerIndex = PlayerIndex.Player1;
    private PlayerIndex m_LastPlayerIndex = PlayerIndex.Invalid;
    private MechMaterialController m_MechMaterialController;

    IPlayerColors Colors => GameController.Instance.PlayerColors;
    int m_ColorVersionId = 0;

    void Start()
    {
        GameObject mechObject = Instantiate(m_MechPrefab, transform);
        m_MechMaterialController = mechObject?.GetComponent<MechMaterialController>();
        UpdateMechIfDirty(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMechIfDirty();
    }

    private void UpdateMechIfDirty(bool force = false)
    {
        if (force ||
            m_LastPlayerIndex != m_PlayerIndex ||
            m_ColorVersionId != Colors.ColorsVersionId)
        {
            m_MechMaterialController?.ApplyPlayerColors(
                GameController.Instance.PlayerColors, m_PlayerIndex);
            m_LastPlayerIndex = m_PlayerIndex;
            m_ColorVersionId = Colors.ColorsVersionId;
        }
    }
}
