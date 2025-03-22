using UnityEngine;

// Stores and manages exam and gender related data
public class DataBanker : MonoBehaviour
{
    // Singleton instance of Databanker to ensure only one exists
    public static DataBanker Instance { get; private set; }

    // Private variables to store exam type and gender
    private string EXAM;
    private string SEX;
    
    // Call awake when script instance is being loaded
    private void Awake()
    {
        // See if DataBanker exists
        if (Instance == null)
        {
            // Assign the instance as the singleton
            Instance = this;
            // Prevent this object from being destroyed when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another instance exists, destroy this duplicate
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    // Set the exam type
    public void SetExamType(string ex)
    {
        EXAM = ex;
    }

    // Set the gender
    public void SetSex(string a)
    {
       SEX = a;
    }

    // get the exam type
    public string GetExamType()
    {
        return EXAM;
    }

    // get the gender
    public string whatSEX()
    {
        return SEX;
    }
}