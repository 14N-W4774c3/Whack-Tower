using UnityEngine;
using TMPro;
public class HelpScreenScript : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameObject.GetComponent<CanvasGroup>().alpha = (gameObject.GetComponent<CanvasGroup>().alpha == 1) ? 0 : 1;
            Debug.Log(gameObject.GetComponent<CanvasGroup>().alpha);
        }
    }
}
