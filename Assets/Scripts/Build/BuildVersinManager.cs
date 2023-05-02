using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildVersinManager : MonoBehaviour
{
    public GameObject ClientGroup;
    public GameObject ServerGroup;

    // Start is called before the first frame update
    void Start()
    {
#if BUILD_SERVER
        ClientGroup.SetActive(false);
        ServerGroup.SetActive(true);
#endif

#if BUILD_CLIENT
        ClientGroup.SetActive(true);
        ServerGroup.SetActive(false);
        print("Running As Client");
#endif


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
