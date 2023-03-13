using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityMirrorExample.Server;

public class DevicesListController : MonoBehaviour
{
    public GameObject panelTitle;
    public GameObject panelPrefab;

    public GameObject k_panelPrefab;

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

        int index = 0;
        foreach (Glasses g in unknownGlasses)
        {
            position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
            instance = Instantiate(panelPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);
            var glassComponent = instance.GetComponent<UGlassesComponent>();

            instance.GetComponent<UGlassesComponent>().DevicesListController = this;
            instance.GetComponent<UGlassesComponent>().Instance = instance;
            glassComponent.SetFields(g.id, g.name, g.status);
            glassComponent.Button.GetComponent<Button>().onClick.AddListener(glassComponent.EditName);
            glassComponent.SaveButton.GetComponent<Button>().onClick.AddListener(glassComponent.FinishEdit);
            print(index);
            index++;
        }




        position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
        instance = Instantiate(knownSignPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);

        foreach (Glasses g in knownGlasses)
        {
            position = new Vector2(instance.transform.position.x, instance.transform.position.y - yDistance);
            instance = Instantiate(k_panelPrefab, position, new Quaternion(0, 0, 0, 1), parent.transform);
            var glassComponent = instance.GetComponent<GlassesComponent>();
            glassComponent.SetFields(g.id, g.name, g.status);

        }
    }

    private void editName(GameObject gameObject, int id)
    {
        print("Editing Glasses with index " + id);
        print("Instance is " + gameObject.GetHashCode());
        //throw new NotImplementedException();
    }


    public void SetKnown(GameObject instance)
    {
        instance.GetComponent<UGlassesComponent>().SetKnown();
    }


    void Update()
    {
       
    }
}
