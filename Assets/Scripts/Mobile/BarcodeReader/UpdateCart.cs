using UnityEngine;
using UnityEngine.UI;

public class UpdateCart : MonoBehaviour
{
    public GameObject cartProductPrefab;
    public GameObject content_productScrollView;

    public void AddToCart()
    {
        GameObject product = Instantiate(cartProductPrefab);
        product.transform.parent = content_productScrollView.transform;
        product.transform.localScale = new Vector3(1, 1, 1);
    }
}
