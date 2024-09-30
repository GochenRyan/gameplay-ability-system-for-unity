using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EquipmentActor != null)
        {
            var go = collision.gameObject;

            var equipable = go.GetComponent<IEquipable>();
            if (equipable != null) 
            {
                equipable.AddEquipment(EquipmentActor);
                GameActor.Instance.DestoryEquipment(EquipmentActor);
            }
        }
    }

    public EquipmentActor EquipmentActor { get; set; }
}
