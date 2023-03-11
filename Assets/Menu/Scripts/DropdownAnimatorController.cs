using UnityEngine;


/// 
/// Add this script to the Template element of the Dropdown list
///
public class DropdownAnimatorController : MonoBehaviour
{

    public GameObject self;
    public GameObject Arrow;

    void Start()
    {
        if (self.name == "Dropdown List")
        {
            Arrow.GetComponent<Animator>().SetTrigger("Animate");
        }
    }

    private void OnDestroy()
    {
        if (self.name == "Dropdown List")
        {
            Arrow.GetComponent<Animator>().SetTrigger("Animate");
        }
    }
}
