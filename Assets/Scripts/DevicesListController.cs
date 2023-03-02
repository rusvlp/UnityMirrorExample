using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DevicesListController : MonoBehaviour
{
    public GameObject panelTitle;
    public GameObject panelPrefab;

    public GameObject parent;

    public float yDistance;

    public GameObject knownSignPrefab;
    public GameObject unknownSignPrefab;

    List<Glasses> unknownGlasses = new List<Glasses>()
    {
        new Glasses(1, "Синие очки", "Новое"),
        new Glasses(2, "Белые очки", "Новое"),
        new Glasses(3, "Желтые очки", "Новое"),
      
    };

    List<Glasses> knownGlasses = new List<Glasses>()
    {
        new Glasses(3, "Желтые очки", "Новое"),
        new Glasses(3, "Желтые очки", "Новое"),
        new Glasses(3, "Желтые очки", "Новое"),
        new Glasses(3, "Желтые очки", "Новое"),
        new Glasses(3, "Желтые очки", "Новое"),
        new Glasses(3, "Желтые очки", "Новое"),
        new Glasses(3, "Желтые очки", "Новое"),
        new Glasses(3, "Желтые очки", "Новое"),
    };

    //[SerializeField] TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        //Vector2 position = new Vector2(panelTitle.transform.position.x, panelTitle.transform.position.y - yDistance);
        GameObject instance = panelTitle;
        Vector2 position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
        instance = Instantiate(unknownSignPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);
        foreach (Glasses g in unknownGlasses)
        {
            position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
            instance = Instantiate(panelPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);
            instance.transform.Find("id_tmp").GetComponent<TMP_Text>().text = g.id + "";
            instance.transform.Find("name_tmp").GetComponent<TMP_Text>().text = g.name;
            instance.transform.Find("status_tmp").GetComponent<TMP_Text>().text = g.status;
        }

        position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
        instance = Instantiate(knownSignPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);

        foreach (Glasses g in knownGlasses)
        {
            position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
            instance = Instantiate(panelPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);
            instance.transform.Find("id_tmp").GetComponent<TMP_Text>().text = g.id + "";
            instance.transform.Find("name_tmp").GetComponent<TMP_Text>().text = g.name;
            instance.transform.Find("status_tmp").GetComponent<TMP_Text>().text = g.status;
        }
    }

    void Update()
    {
        
    }
}
