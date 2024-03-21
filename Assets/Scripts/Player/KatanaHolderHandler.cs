using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaHolderHandler : MonoBehaviour
{
    [SerializeField] private GameObject katanaInHand;
    [SerializeField] private GameObject katanaOnSheath;

    private bool isKatanaDrawn = false;

    public void DrawOrSheathKatana()
    {
        if (isKatanaDrawn)
        {
            katanaInHand.SetActive(false);
            katanaOnSheath.SetActive(true);
            isKatanaDrawn = false;
        }
        else
        {
            katanaInHand.SetActive(true);
            katanaOnSheath.SetActive(false);
            isKatanaDrawn = true;
        }
    }
}
