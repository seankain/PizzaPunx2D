using UnityEngine;
using UnityEngine.UI;

public class UserFeedback : MonoBehaviour
{
    public Text targetTextbox;
    public float lifetime = 1.5f;
    public float yFloatSpeed = 0.1f;
    public Vector2 angleMinMax = new Vector2(-45f, 45f);
    public Vector2 xMinMax = new Vector2(3f, 5f);
    public Vector2 yMinMax = new Vector2(-3f, -2.5f);

    Color startColor = Color.blue;
    Color endColor;
    float elaped = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (targetTextbox == null) Debug.LogError("Text box is empty?");
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(angleMinMax.x, angleMinMax.y));
        transform.position = new Vector3(Random.Range(xMinMax.x, xMinMax.y), Random.Range(yMinMax.x, yMinMax.y), 0);
    }

    public void SetText(string text, Color textColor)
    {
        startColor = textColor;
        endColor = textColor;
        endColor.a = 0f;
        targetTextbox.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        elaped += Time.deltaTime;
        transform.Translate(0, yFloatSpeed + Time.deltaTime, 0, Space.World);
        targetTextbox.color = Color.Lerp(startColor, endColor, elaped / lifetime);
        if (elaped > lifetime) Destroy(gameObject);
    }
}
