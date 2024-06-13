using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollision : MonoBehaviour
{
    public Hint[] hint;
    public bool isTriggerDestroy = false;
    public bool isTriggerDeactiveCollision = false;
    public bool isDestroyTriggerObject = false;
    public bool isAddPoin = false;
    [SerializeField] private string triggerObjectName;

    [Header("Spawn Object")]
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private GameObject spawnParent;

    [Header("Activate Object")]
    [SerializeField] private GameObject objectToActivated;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == triggerObjectName)
        {
            if (spawnObject) OnSpawn();
            if (objectToActivated) OnActivated();

            if (isAddPoin) AddPoin();

            if (isTriggerDestroy) Destroy(gameObject);

            if (isDestroyTriggerObject) TriggerObjectDestroy();

            if (isTriggerDeactiveCollision)
            {
                BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
                collider.enabled = false;
            }
        }
    }

    public void AddPoin()
    {
        TriggerObject.instance.poin += 1;
        // TriggerObject.instance.triggerEvent -= AddPoin;
    }

    public void TriggerObjectDestroy()
    {
        GameObject triggerObject = GameObject.Find(triggerObjectName);
        Destroy(triggerObject);
        // TriggerObject.instance.triggerEvent -= TriggerObjectDestroy;
    }

    public void OnSpawn()
    {
        GameObject newSpawn = Instantiate(spawnObject, spawnPosition.transform.position, Quaternion.identity, spawnParent.transform);

        // TriggerObject.instance.triggerEvent -= OnSpawn;
    }

    public void OnActivated()
    {
        objectToActivated.SetActive(true);

        // TriggerObject.instance.triggerEvent -= OnActivated;
    }
}
