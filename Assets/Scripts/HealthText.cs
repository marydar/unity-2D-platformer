using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    public float timeToFade = 1f;
    private float timeElapsed = 0f;
    private Color startColor;
    TextMeshProUGUI textMeshPro;
    RectTransform textTransform;
    void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if(timeElapsed < timeToFade){
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a * (1 - (timeElapsed / timeToFade)));
        }
        else{
            Destroy(gameObject);
        }
    }
}
