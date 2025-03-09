using UnityEngine;
public class DataBanker : MonoBehaviour
{
    public static DataBanker Instance { get; private set; }

    private string EXAM;
    private string SEX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    public void SetExamType(string ex)
    {
        EXAM = ex;
    }

    public void SetSex(string a)
    {
       SEX = a;
    }

    public string GetExamType()
    {
        return EXAM;
    }

    public string whatSEX()
    {
        return SEX;
    }
}