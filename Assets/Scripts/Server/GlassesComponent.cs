using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UnityMirrorExample.Server
{
    public class GlassesComponent : MonoBehaviour
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Status { get; private set; }


        [SerializeField] TMP_Text text_id;
        [SerializeField] TMP_Text text_name;
        [SerializeField] TMP_Text text_status;

        public void SetFields(long id, string name, string status)
        {
            Id = id;
            Name = name;
            Status = status;

            text_id.text = id.ToString();
            text_name.text = name;
            text_status.text = status;
        }
        public void EditName()
        {
            print(Name);
        }
    }
}

