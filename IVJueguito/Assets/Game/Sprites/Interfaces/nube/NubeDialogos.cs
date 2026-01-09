using UnityEngine;
using UnityEngine.UI;


public class NubeDialogos : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Image image;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.sprite = spriteRenderer.sprite;
    }
}
