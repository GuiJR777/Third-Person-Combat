using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaHolderHandler : MonoBehaviour
{
    [SerializeField] private GameObject katanaInHand;
    [SerializeField] private GameObject katanaOnSheath;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void DrawOrSheathKatana()
    {
        bool isKatanaDrawn = playerController.Data.AnimationData.IsKatanaDrawn;
        katanaInHand.SetActive(isKatanaDrawn);
        katanaOnSheath.SetActive(!isKatanaDrawn);
    }
}
