using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FruitDisplay : MonoBehaviour
{
    public FruitObj fruit;

    public Image spriteImage;

    void Start()
    {
        spriteImage.sprite = fruit.GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
