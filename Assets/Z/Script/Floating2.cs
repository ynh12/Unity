using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Floating2 : MonoBehaviour
{
    float size;
    public TMP_Text FloatTextPrint;
    RectTransform rect;
    Rigidbody2D rb;
    // Start is called before the first frame update

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1f);
        FloatTextPrint.text = Enemy2.damage.ToString();
        size = ((float)Enemy2.damage / 100f) + 0.5f;
        size = Mathf.Min(size, 8);
        FloatTextPrint.fontSize = size;
        rect = GetComponent<RectTransform>();
        rect.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-1f, 1f), -1);
        rb.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(12f, 15f));
    }

    void Update()
    {

    }
}
