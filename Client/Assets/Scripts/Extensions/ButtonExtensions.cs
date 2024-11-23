using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class ButtonExtensions
{
    public static void SetListener(this Button button, UnityAction call)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(call);
    }
}
