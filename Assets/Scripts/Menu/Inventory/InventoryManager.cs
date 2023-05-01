using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Product> products;
    public List<Product> selectedProducts;

    public void addProduct()//Product product)
    {
        //products.Add(product);

        GameObject content = GetComponentInChildren<ScrollRect>().content.gameObject;

        GameObject togglePrefab = GameObject.Find("BaseToggle");

        GameObject newToggle = Instantiate(togglePrefab, content.transform);

        newToggle.SetActive(true);

        //GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 0f;

    }

    public void removeProduct(Product product)
    {
        products.Remove(product);
    }

    public void addSelectedProduct(Product product)
    {
        selectedProducts.Add(product);
    }

    public void removeSelectedProduct(Product product)
    {
        selectedProducts.Remove(product);
    }

    public void deleteSelectedProducts()
    {
        foreach (Product product in selectedProducts)
        {
            //Delete(product.product_id);
            Debug.Log("Silindi: " + product.product_name);

        }

        selectedProducts.Clear();
    }

}
