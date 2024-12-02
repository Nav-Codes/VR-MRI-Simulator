using UnityEngine;

public class SetActiveMenu : MonoBehaviour
{
    // public game objects to control
    public GameObject[] initGOs;

    public GameObject[] finalGOs;

    private string[] selection1Names;
    private string[] selection2Names;
    
    private int menu1Index = 0; // tracks which menu 1 opt selected    
    private int menu2Index = 0; // tracks which menu 2 opt selected

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // coil names
        selection1Names = new string[initGOs.Length + 1];
        selection1Names[0] = "None";
        for (int i = 0; i < initGOs.Length; i++)
        {
            selection1Names[i + 1] = initGOs[i].name;
        }

        // patient position names
        selection2Names = new string[finalGOs.Length + 1];
        selection2Names[0] = "None";
        for (int i = 0; i < finalGOs.Length; i++)
        {
            selection2Names[i + 1] = finalGOs[i].name;
        }
    }

    // sample GUIlayout buttons
    // this would be replaced by the VR UI menu
    void OnGUI()
    {
        // PoC create a selection grid
        
        GUILayout.Label("Select 1st option");

        menu1Index = GUILayout.SelectionGrid(menu1Index, selection1Names, 5 );
        if (menu1Index > 0)
        {
            // show patient position 
            // add blank space
            GUILayout.Space(20);
            GUILayout.Label("Select 2nd option");
            menu2Index = GUILayout.SelectionGrid(menu2Index, selection2Names, 5);
        }

        if (menu1Index == 0) {
            menu2Index = 0;
        }
        
        // set active/inactive based on menu selection
        for (int i = 0; i < initGOs.Length; i++)
        {
           
            if (menu1Index == i + 1 && menu2Index == 0)
            {
                initGOs[i].SetActive(true);
            }
            else
            {
                initGOs[i].SetActive(false);
            }
        }

        for (int i = 0; i < finalGOs.Length; i++)
        {
            if (menu2Index == i + 1)
            {
                finalGOs[i].SetActive(true);
            }
            else
            {
                finalGOs[i].SetActive(false);
            }
        }

        // add blank space
        GUILayout.Space(20);

        // button to reset menuIndex
        if (GUILayout.Button("Reset"))
        {
            menu1Index = 0;
            menu2Index = 0;
        }
    }
}
