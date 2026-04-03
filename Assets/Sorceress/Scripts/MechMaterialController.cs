using System;
using System.Collections.Generic;
using UnityEngine;

public class MechMaterialController : MonoBehaviour
{
    private Material m_Material;
    [SerializeField] private float m_EmissiveStrength = 100.0f;
    [SerializeField] private Color m_ColorMask1 = Color.cyan;
    [SerializeField] private Color m_ColorMask2 = Color.black;

    private static readonly string k_EnableColorMaskingProp = "_Enable_Color_Masking";
    private static readonly string k_PrimaryColorProp = "_Color_Swap_0";
    private static readonly string k_SecondaryColorProp = "_Color_Swap_1";
    private static readonly string k_PrimaryEmissiveColorMaskProp = "_EmissionColor_1";
    private static readonly string k_PrimaryEmissiveColorProp = "_EmissionColor_2";
    private static readonly string k_SecondaryEmissiveColorMaskProp = "_EmissionColor_3";
    private static readonly string k_SecondaryEmissiveColorProp = "_EmissionColor_4";
    private static readonly string k_EmissionStrengthProp = "_Emission_Base_Multiplier";

    private float m_LastEmissiveStrength = 0;
    private Color m_LastColorMask1 = Color.black;
    private Color m_LastColorMask2 = Color.black;

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

        SetMaterialEmissiveStrength(m_EmissiveStrength);
    }

    public void ApplyPlayerColors(IPlayerColors playerColors, PlayerIndex playerIndex)
    {
        m_Material.SetInt(k_EnableColorMaskingProp, PlayerIndexUtil.IsValid(playerIndex) ? 1 : 0);
        m_Material.SetColor(k_PrimaryColorProp, playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Primary));
        m_Material.SetColor(k_SecondaryColorProp, playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Secondary));
        m_Material.SetColor(k_PrimaryEmissiveColorProp, playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.EmissivePrimary));
        m_Material.SetColor(k_SecondaryEmissiveColorProp, playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.EmissiveSecondary));
    }

    void Update()
    {
        if (m_EmissiveStrength != m_LastEmissiveStrength)
        {
            SetMaterialEmissiveStrength(m_EmissiveStrength);
        }

        if (m_ColorMask1 != m_LastColorMask1)
        {
            m_Material.SetColor(k_PrimaryEmissiveColorMaskProp, m_ColorMask1);
            m_LastColorMask1 = m_ColorMask1;
        }

        if (m_ColorMask2 != m_LastColorMask2)
        {
            m_Material.SetColor(k_SecondaryEmissiveColorMaskProp, m_ColorMask2);
            m_LastColorMask2 = m_ColorMask2;
        }
    }

    private void SetMaterialEmissiveStrength(float strength)
    {
        m_Material.SetFloat(k_EmissionStrengthProp, strength);
        m_LastEmissiveStrength = m_EmissiveStrength;
    }
}
