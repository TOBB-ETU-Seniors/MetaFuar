using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
    public static KeyboardManager Instance;
    public TMP_InputField textBox;
    public Button submitButton;

    private void Start()
    {
        Instance = this;
        textBox.text = "";
    }

    public void DeleteLetter()
    {
        if (textBox.text.Length != 0)
        {
            textBox.text = textBox.text.Remove(textBox.text.Length - 1, 1);
        }
    }

    public void AddLetter(string letter)
    {
        textBox.text = textBox.text + letter;
    }

    public void SubmitWord()
    {
        submitButton.Select();
        // Debug.Log("Text submitted successfully!");
    }
}
