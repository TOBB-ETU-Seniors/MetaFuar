using UnityEngine;

public class PanelGroup : MonoBehaviour
{
    public GameObject[] panelGroup;

    void Start()
    {
        DisableAllPanelsExceptGivenInParameter(panelGroup[0].gameObject);
    }

    public void DisableAllPanelsExceptGivenInParameter(GameObject exceptPanel)
    {
        foreach (GameObject panel in panelGroup)
        {
            if (panel.name.Equals(exceptPanel.name))
                panel.SetActive(true);
            else
                panel.SetActive(false);
        }
    }

}
