using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance => s_Instance;
    private static GameController s_Instance = null;
    private PlayerColors m_PlayerColors;
    public IPlayerColors PlayerColors => m_PlayerColors;

    void Awake()
    {
        Debug.Assert(s_Instance == null);
        s_Instance = this;

        m_PlayerColors = GetComponent<PlayerColors>();
    }
}
