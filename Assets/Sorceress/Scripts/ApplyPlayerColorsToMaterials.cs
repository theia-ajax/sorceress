using System.Collections.Generic;
using UnityEngine;

public class ApplyPlayerColorsToMaterials : MonoBehaviour
{
    List<Material> m_Materials = new();

    void Awake()
    {
        foreach (var renderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            m_Materials.AddRange(renderer.materials);
        }
        foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
        {
            m_Materials.AddRange(renderer.materials);
        }
    }

    public void ApplyPlayerColors(IPlayerColors playerColors, PlayerIndex playerIndex)
    {
        if (PlayerIndexUtil.IsValid(playerIndex))
        {
            foreach (var material in m_Materials)
            {
                material.SetInt("_Enable_Color_Masking", 1);
                material.SetColor("_Color_Swap_0", playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Primary));
                material.SetColor("_Color_Swap_1", playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Secondary));
                material.SetColor("_EmissionColor_2", playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Primary));
            }
        }
        else
        {
            foreach (var material in m_Materials)
            {
                material.SetInt("_Enable_Color_Masking", 0);
            }
        }
    }
}
