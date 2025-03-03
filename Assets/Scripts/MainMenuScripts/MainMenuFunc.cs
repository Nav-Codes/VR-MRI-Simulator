using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Add this namespace for TextMeshPro support

public class MainMenuFunc : MonoBehaviour
{
    public GameObject canvas1; // Assign in Inspector
    public GameObject canvas2; // Assign in Inspector

    public void SwitchCanvas()
    {
        canvas1.SetActive(!canvas1.activeSelf);
        canvas2.SetActive(!canvas2.activeSelf);
    }

    public void OnButtonClicked(Button button)
    {
        if (button == null)
        {
            Debug.LogError("Button is null!");
            return;
        }

        Debug.Log("Button Clicked: " + button.name);

        // Use TextMeshProUGUI instead of Text
        TextMeshProUGUI buttonTextComponent = button.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonTextComponent == null)
        {
            Debug.LogError("TextMeshProUGUI component not found in button children!");
            return;
        }

        string buttonText = buttonTextComponent.text; // Get button text
        Debug.Log("Button Text: " + buttonText);

        if (buttonText == "Male" || buttonText == "Female") // Fixed condition
        {
            if (buttonText == "Male")
            {
                DataBanker.Instance.SetMale(true);
                SwitchCanvas();
            }
            else
            {
                DataBanker.Instance.SetMale(false);
                SwitchCanvas();
            }
        }
        else
        {
            examCheck(buttonText); // This will now be called correctly
        }
    }

    public void examCheck(string check)
    {
        Debug.Log("Exam Check called with: " + check);
        DataBanker.Instance.SetExamType(check);
        SceneManager.LoadScene("Room4");
    }
}