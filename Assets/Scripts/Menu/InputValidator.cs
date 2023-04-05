using System.Text.RegularExpressions;
using UnityEngine;

public class InputValidator : MonoBehaviour
{
    /*public InputField inputField;

    private void Start()
    {
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    private void ValidateInput(string input)
    {
        if (input.Length < 5)
        {
            Debug.LogError("Input is too short.");
        }
        else
        {
            Debug.Log("Input is valid.");
        }
    }*/
    // Example pattern: starts with a letter, contains only letters, numbers, and underscores, and is between 6 and 20 characters long
    const string RegexValidUsername = "^[a-zA-Z][a-zA-Z0-9_]{5,19}$";
    // Example pattern: contains a word, @ symbol, and a word, followed by a . and a word (e.g. example@example.com)
    const string RegexValidEmail = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
    // Example pattern: at least 8 characters long, contains at least one lowercase letter, one uppercase letter, and one digit
    const string RegexValidPassword = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{7,}$";

    // Validate username
    public string ValidateUsername(string username)
    {
        string errorMessage = "";
        if (!Regex.IsMatch(username, RegexValidUsername))
        {
            errorMessage = "Lütfen geçerli bir kullanýcý adý giriniz.";
        }
        return errorMessage;
    }

    // Validate email
    public string ValidateEmail(string email)
    {
        string errorMessage = "";
        if (!Regex.IsMatch(email, RegexValidEmail))
        {
            errorMessage = "Lütfen geçerli bir mail giriniz.";
        }
        return errorMessage;
    }

    // Validate password
    public string ValidatePassword(string password)
    {
        string errorMessage = "";
        if (!Regex.IsMatch(password, RegexValidPassword))
        {
            errorMessage = "Lütfen geçerli bir parola giriniz.";
        }
        return errorMessage;
    }

    public string ValidateEmailOrUsername(string value)
    {
        string errorMessage;
        if (value.Contains("@"))
            errorMessage = ValidateEmail(value);
        else
            errorMessage = ValidateUsername(value);

        return errorMessage;
    }
}
