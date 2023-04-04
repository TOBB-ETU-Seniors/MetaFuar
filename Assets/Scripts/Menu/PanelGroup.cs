using UnityEngine;
using UnityEngine.UI;

public class PanelGroup : MonoBehaviour
{
    [SerializeField] private GameObject[] panelGroup;
    [SerializeField] private Toggle[] toggleGroup;


    void OnEnable()
    {
        foreach (var toggle in toggleGroup)
        {
            toggle.isOn = false;
        }
        toggleGroup[0].isOn = true;
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
