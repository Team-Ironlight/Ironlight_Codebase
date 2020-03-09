using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveCredits : MonoBehaviour
{
    Text CurrText;
    public Text nextText;
    Vector2 MoveUp;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        CurrText = GetComponent<Text>();
        MoveUp = CurrText.transform.position;
        nextText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        TextScroll();
        if (CurrText.transform.position.y >= 50)
        {
            nextText.enabled = true;
        }
    }
    void TextScroll()
    {
        CurrText.transform.Translate(transform.up * Time.deltaTime * speed);
    }
}
