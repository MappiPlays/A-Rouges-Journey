using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    private void OnEnable()
    {
        FindObjectOfType<EventSystem>().SetSelectedGameObject(GetComponentInChildren<Button>().gameObject);
    }
}
