using System.Collections.Generic;
using UnityEngine;

public class MechMaterialController : MonoBehaviour
{
    private Material m_Material;
    private float m_EmissiveStrength = 100.0f;
    private bool m_EmissiveEnabled = true;
    [SerializeField] float m_EmissiveSmoothTime = 0.1f;

    private static readonly string k_EnableColorMaskingProp = "_Enable_Color_Masking";
    private static readonly string k_PrimaryColorProp = "_Color_Swap_0";
    private static readonly string k_SecondaryColorProp = "_Color_Swap_1";
    private static readonly string k_EmissiveColorMaskProp = "_EmissionColor_1";
    private static readonly string k_EmissiveColorProp = "_EmissionColor_2";
    private static readonly string k_EmissionStrengthProp = "_Emission_Base_Multiplier";

    void Awake()
    {

        foreach (var renderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (m_Material == null)
            {
                m_Material = renderer.material;
            }
            else
            {
                renderer.material = m_Material;
            }
        }
        foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
        {
            if (m_Material == null)
            {
                m_Material = renderer.material;
            }
            else
            {
                renderer.material = m_Material;
            }
        }

        m_TargetEmissiveStrength = m_EmissiveStrength;
    }

    public void ApplyPlayerColors(IPlayerColors playerColors, PlayerIndex playerIndex)
    {
        m_Material.SetInt(k_EnableColorMaskingProp, PlayerIndexUtil.IsValid(playerIndex) ? 1 : 0);
        m_Material.SetColor(k_PrimaryColorProp, playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Primary));
        m_Material.SetColor(k_SecondaryColorProp, playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Secondary));
        m_Material.SetColor(k_EmissiveColorProp, playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Emissive));
    }

    readonly float[] k_EmissiveStrengths = new float[]
    {
        0f, 10f, 25f, 50f, 100f, 250f, 500f, 1000f, 2500f, 5000f, 10000f, 25000f, 50000f, 100000f
    };
    private int m_EmissiveStrengthIndex = 4;
    private float m_TargetEmissiveStrength = 0f;
    private float m_EmissiveVelocity = 0f;

    readonly Color[] k_EmissiveColorMasks = new Color[]
    {
        Color.black,
        Color.white,
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.cyan,
        new Color(0.274509f, 0.274509f, 0.274509f)
    };
    private int m_EmissiveColorMaskIndex = 3;

    void OnGUI()
    {
        if (GUI.Button(new Rect(5, 5, 80, 20), "Toggle"))
        {
            SetEmissiveEnabled(!m_EmissiveEnabled);
        }

        if (GUI.Button(new Rect(5, 30, 20, 20), "-"))
        {
            m_EmissiveStrengthIndex = Mathf.Max(m_EmissiveStrengthIndex - 1, 0);
            m_TargetEmissiveStrength = k_EmissiveStrengths[m_EmissiveStrengthIndex];
        }
        if (GUI.Button(new Rect(30, 30, 20, 20), "+"))
        {
            m_EmissiveStrengthIndex = Mathf.Min(m_EmissiveStrengthIndex + 1, k_EmissiveStrengths.Length - 1);
            m_TargetEmissiveStrength = k_EmissiveStrengths[m_EmissiveStrengthIndex];
        }

        if (GUI.Button(new Rect(5, 55, 20, 20), "-"))
        {
            m_EmissiveColorMaskIndex = Mathf.Max(m_EmissiveColorMaskIndex - 1, 0);
            m_Material.SetColor(k_EmissiveColorMaskProp, k_EmissiveColorMasks[m_EmissiveColorMaskIndex]);
        }
        if (GUI.Button(new Rect(30, 55, 20, 20), "+"))
        {
            m_EmissiveColorMaskIndex = Mathf.Min(m_EmissiveColorMaskIndex + 1, k_EmissiveColorMasks.Length - 1);
            m_Material.SetColor(k_EmissiveColorMaskProp, k_EmissiveColorMasks[m_EmissiveColorMaskIndex]);
        }
    }

    void Update()
    {
        m_EmissiveStrength = Mathf.SmoothDamp(m_EmissiveStrength, m_TargetEmissiveStrength, ref m_EmissiveVelocity, m_EmissiveSmoothTime);
        SetEmissiveStrength(m_EmissiveStrength);
    }

    public void SetEmissiveEnabled(bool enabled)
    {
        if (m_EmissiveEnabled != enabled)
        {
            m_EmissiveEnabled = enabled;
            m_TargetEmissiveStrength = m_EmissiveEnabled ? k_EmissiveStrengths[m_EmissiveStrengthIndex] : 0f;
            // SetEmissiveStrength(m_EmissiveEnabled ? m_EmissiveStrength : 0f);
        }
    }

    private void SetEmissiveStrength(float strength)
    {
        m_Material.SetFloat(k_EmissionStrengthProp, strength);
    }
}
