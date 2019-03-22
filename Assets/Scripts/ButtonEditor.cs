using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class ButtonEditor : MonoBehaviour
{
    private TextMeshProUGUI _tmp;

    public string buttonText = "N/A";
    public float fontSize = 60f;

    void Start()
    {
        _tmp = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (_tmp == null)
            Debug.Log("WTF");
        SetTMPDefaults();
    }

    void OnValidate()
    {
        _tmp.text = buttonText;
        _tmp.fontSize = fontSize;
    }

    void SetTMPDefaults()
    {
        _tmp.fontStyle = FontStyles.Bold;
        _tmp.color = Color.black;
    }
}
