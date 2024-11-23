using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILevelUpPopupSlot : MonoBehaviour
{
    [SerializeField] private Button _slotButton;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _descriptionText;

    public void Bind(UnityAction onClick, Sprite icon, string name, string description)
    {
        _slotButton.SetListener(onClick);
        _iconImage.sprite = icon;
        _nameText.text = name;
        _descriptionText.text = description;
    }
}
