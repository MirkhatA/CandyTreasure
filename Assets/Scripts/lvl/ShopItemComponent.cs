using TMPro;
using UnityEngine;

public class ShopItemComponent : MonoBehaviour
{
    [SerializeField] private GameObject lockObject;
    [SerializeField] private int bgId;
    [SerializeField] private float price;
    [SerializeField] private TMP_Text priceText;

    [SerializeField] private GameObject selectedItem;

    private bool _hasItem;
    private float _money;

    private void Start()
    {
        selectedItem.SetActive(false);

        _money = PlayerPrefs.GetFloat("money");
        _hasItem = PlayerPrefs.GetInt("hasBgWithId" + bgId) == 1;

        if (priceText != null) priceText.text = price.ToString();

        DoesHaveItem();
    }

    private void DeactivateAllSelectedItems() {
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.name == "SelectedIcon")
            {
                obj.SetActive(false);
            }
        }
    }

    public void SetItemBGPref()
    {
        if (_hasItem)
        {
            PlayerPrefs.SetInt("backgroundIndex", bgId);
            DeactivateAllSelectedItems();
            selectedItem.SetActive(true);
        }
        else
        {
            if (_money >= price)
            {
                _money -= price;
                PlayerPrefs.SetFloat("money", _money);
                PlayerPrefs.SetInt("hasBgWithId" + bgId, 1);
                PlayerPrefs.SetInt("backgroundIndex", bgId);
                DeactivateAllSelectedItems();
                DoesHaveItem();
                selectedItem.SetActive(true);
            }
        }
    }

    public void DoesHaveItem()
    {
        _hasItem = PlayerPrefs.GetInt("hasBgWithId" + bgId) == 1;

        if (lockObject != null && _hasItem)
        {
            lockObject.SetActive(false);
        }
    }
}
