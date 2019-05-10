using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class SkillViewPort : MonoBehaviour, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        ObjectManager.SkillIntroPanelGo.SetActive(false);
    }
}
