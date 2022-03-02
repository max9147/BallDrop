using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAP : MonoBehaviour, IStoreListener
{
    public Button vipButton;
    public TextMeshProUGUI vipText;

    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;
    private static string VIPpayID = "vip";

    private bool isVIP = false;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("VIP"))
        {
            isVIP = true;
            vipText.text = "Thanks!";
            vipButton.interactable = false;
        }
        else
        {
            isVIP = false;
        }
    }

    private void Start()
    {
        if (storeController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (storeController != null && extensionProvider != null)
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(VIPpayID, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyVIP()
    {
        if (storeController != null && extensionProvider != null)
        {
            Product product = storeController.products.WithID(VIPpayID);
            if (product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product);
            }
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, VIPpayID, StringComparison.Ordinal))
        {
            if (!PlayerPrefs.HasKey("VIP"))
            {
                PlayerPrefs.SetInt("VIP", 0);
                isVIP = true;
                vipText.text = "Thanks!";
                GetComponent<AdDouble>().SetVIP();
            }
        }
        return PurchaseProcessingResult.Complete;
    }

    public bool GetVIPStatus()
    {
        return isVIP;
    }
}