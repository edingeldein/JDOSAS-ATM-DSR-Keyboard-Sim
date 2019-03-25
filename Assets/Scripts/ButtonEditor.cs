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
        if (_tmp is null) return;
        _tmp.text = buttonText;
        _tmp.fontSize = fontSize;
    }

    void SetTMPDefaults()
    {
        if (_tmp is null) return;
        _tmp.fontStyle = FontStyles.Bold;
        _tmp.color = Color.black;
    }
}
