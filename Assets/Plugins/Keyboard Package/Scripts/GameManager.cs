using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_InputField textBox;
    public Button submitButton;

    private void Start()
    {
        Instance = this;
    }

    public void SetTextBox(TMP_InputField textBox)
    {
        this.textBox = textBox;
    }

    public void SetSubmitButton(Button submitButton)
    {
        this.submitButton = submitButton;
    }

    public void DeleteLetter()
    {
        if (textBox != null)
        {
            if (textBox.text.Length != 0)
            {
                textBox.text = textBox.text.Remove(textBox.text.Length - 1, 1);
            }
        }
    }

    public void AddLetter(string letter)
    {
        if (textBox != null)
        {
            textBox.text = textBox.text + letter;
        }
    }

    public void SubmitWord()
    {
        if (submitButton != null)
        {
            submitButton.Select();
            // Debug.Log("Text submitted successfully!");
        }
    }
}
