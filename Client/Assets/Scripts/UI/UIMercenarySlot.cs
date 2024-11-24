using UnityEngine;
using UnityEngine.UI;

public class UIMercenarySlot : MonoBehaviour
{
    [SerializeField] private Image _mercenaryImage;
    [SerializeField] private Image _skillImage;
    public Image MercenaryImage => _mercenaryImage;

    public void Bind(Sprite mercenarySprite, Sprite skillSprite)
    {
        _mercenaryImage.sprite = mercenarySprite;
        _skillImage.sprite = skillSprite;
    }
}
