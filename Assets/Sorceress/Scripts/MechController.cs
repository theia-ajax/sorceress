using UnityEngine;
using UnityEngine.InputSystem;

public class MechController : MonoBehaviour
{
    private InputAction m_MoveAction;
    private PlayerInput m_PlayerInput;
    private Animator m_Animator;

    void Awake()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_PlayerInput = GetComponent<PlayerInput>();
        m_MoveAction = m_PlayerInput.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 moveInput = m_MoveAction.ReadValue<Vector2>();


        m_Animator.SetFloat("MoveX", moveInput.x);
        m_Animator.SetFloat("MoveY", moveInput.y);
        m_Animator.SetBool("HasMoveInput", moveInput != Vector2.zero);
    }

    void OnGUI()
    {
        Vector2 move = m_MoveAction.ReadValue<Vector2>();
        GUI.Label(new Rect(5, 5, 100, 20), $"{move}");
    }

    void FixedUpdate()
    {
        
    }

    public void OnControlsChanged(PlayerInput playerInput)
    {
        m_PlayerInput = playerInput;
    }
}
