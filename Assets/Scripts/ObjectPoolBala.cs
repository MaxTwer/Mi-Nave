using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBala : MonoBehaviour
{
    public static ObjectPoolBala SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

void Awake()
{
    SharedInstance = this;
}   

// Start is called before the first frame update    

void Start(){
    pooledObjects = new List<GameObject>();
    GameObject tmp;
    for(int i=0; i< amountToPool;i++){
        tmp=Instantiate(objectToPool);
        tmp.SetActive(false);
        pooledObjects.Add(tmp);
    }
}

public GameObject GetPooledObjectB (){
    for(int i=0; i< amountToPool;i++){
        if(!pooledObjects[i].activeInHierarchy){
            return pooledObjects[i];
        }
    }
    return null;
}
}