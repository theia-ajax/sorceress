using System.Collections.Generic;
using UnityEngine;

public class ApplyPlayerColorsToMaterials : MonoBehaviour
{
    List<Material> m_Materials = new();

    void Awake()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            m_Materials.AddRange(renderer.materials);
        }
    }

    public void ApplyPlayerColors(IPlayerColors playerColors, PlayerIndex playerIndex)
    {
        foreach (var material in m_Materials)
        {
            material.SetColor("_Color_Swap_0", playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Primary));
            material.SetColor("_Color_Swap_1", playerColors.GetPlayerColor(playerIndex, PlayerColorSlot.Secondary));
        }
    }
}
