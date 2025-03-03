using UnityEngine;
public class DataBanker : MonoBehaviour
{
    public static DataBanker Instance { get; private set; }

    private string EXAM;
    private bool MALE;

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

    public void SetMale(bool a)
    {
        MALE = a;
    }

    public string GetExamType()
    {
        return EXAM;
    }

    public bool IsMale()
    {
        return MALE;
    }
}