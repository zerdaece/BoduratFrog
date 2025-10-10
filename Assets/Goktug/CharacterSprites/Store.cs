using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour

{
    /*
    public PlayerDress[] dresses;
    public PlayerHead[] heads;
    public int selectedDressIndex = 0;
    public int selectedHeadIndex = 0;
    public GridLayoutGroup dressGrid;
    public GridLayoutGroup headGrid;
    public GameObject storeItemPrefab;

    public Sprite PlayerHeadSprite;
    public Sprite PlayerDressSprite;


    void Start()
    {
        populateHeads();
        populateDresses();
    }

    private void populateHeads()
    {
        foreach (var head in heads)
        {
            GameObject newItem = Instantiate(storeItemPrefab, headGrid.transform);
            StoreItem storeItem = newItem.GetComponent<StoreItem>();
            storeItem.itemImage.sprite = head.BallPose; // Assuming BallPose is the default sprite
            storeItem.itemName.text = head.HeadName;
            storeItem.isAcquired = head.acquired;
            storeItem.selectButton.onClick.AddListener(() => SelectHead(head));
            storeItem.buyButton.onClick.AddListener(() => BuyHead(head, storeItem));
            storeItem.priceText.text = head.acquired ? "Owned" : "100 Coins"; // Example price
            storeItem.buyButton.interactable = !head.acquired;
        }
    }

    private void populateDresses()
    {
        foreach (var dress in dresses)
        {
            GameObject newItem = Instantiate(storeItemPrefab, dressGrid.transform);
            StoreItem storeItem = newItem.GetComponent<StoreItem>();
            storeItem.itemImage.sprite = dress.BallPose; // Assuming BallPose is the default sprite
            storeItem.itemName.text = dress.DressName;
            storeItem.isAcquired = dress.acquired;
            storeItem.selectButton.onClick.AddListener(() => SelectDress(dress));
            storeItem.buyButton.onClick.AddListener(() => BuyDress(dress, storeItem));
            storeItem.priceText.text = dress.acquired ? "Owned" : "100 Coins"; // Example price
            storeItem.buyButton.interactable = !dress.acquired;
        }
    }

    private void BuyHead(PlayerHead head, StoreItem storeItem)
    {
        // Implement your coin deduction logic here
        // For example, if the player has enough coins:
        head.acquired = true;
        storeItem.isAcquired = true;
        storeItem.priceText.text = "Owned";
        storeItem.buyButton.interactable = false;
    }
    private void BuyDress(PlayerDress dress, StoreItem storeItem)
    {
        // Implement your coin deduction logic here
        // For example, if the player has enough coins:
        dress.acquired = true;
        storeItem.isAcquired = true;
        storeItem.priceText.text = "Owned";
        storeItem.buyButton.interactable = false;
    }
    private void SelectHead(PlayerHead head)
    {
        if (head.acquired)
        {
            PlayerHeadSprite = head.IdlePose;
            PlayerSpriteHandler.Instance.head = head;
            Debug.Log("Selected Head: " + head.HeadName);
        }
    }
    private void SelectDress(PlayerDress dress)
    {
        if (dress.acquired)
        {
            PlayerDressSprite = dress.IdlePose;
            PlayerSpriteHandler.Instance.dress = dress;
            Debug.Log("Selected Dress: " + dress.DressName);
        }
    }
}

public class StoreItem
{
    public Image itemImage;
    public Text itemName;
    public Button selectButton;
    public Button buyButton;
    public Text priceText;
    public bool isAcquired;
/*/}
