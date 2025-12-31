using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePool : MonoBehaviour, IObjectPool
{
    [SerializeField]
    private int initialSize = 40;
    private Queue<Slime> slimePoolList;

    [SerializeField]
    private GameObject slimePrefab;

    public void Awake()
    {
        slimePoolList = new Queue<Slime>(initialSize);
        StartPool();
    }


    public void StartPool()
    {
        for (int i = 0; i<initialSize ; i++ )
        {
            GameObject slimeTempGO = Instantiate(slimePrefab);
            Slime slimeTemp = slimeTempGO.GetComponent<Slime>();
            slimeTemp.parentSlimePool = this;
            slimeTemp.ResetObject();
            slimeTemp.SetActive(false);
            slimePoolList.Enqueue(slimeTemp);
        }
    }


    public IPoolObject TakeFromPool()
    {
        if( slimePoolList.Count == 0)
        {
            GameObject slimeTempGO = Instantiate(slimePrefab);
            Slime slimeTemp = slimeTempGO.GetComponent<Slime>();
            slimeTemp.parentSlimePool = this;
            slimeTemp.ResetObject();
            slimeTemp.SetActive(true);
            return slimeTemp;
        }
        else
        {
            Slime slimeTemp = slimePoolList.Dequeue();
            slimeTemp.parentSlimePool = this;
            slimeTemp.ResetObject();
            slimeTemp.SetActive(true);
            return slimeTemp;
        }
    }


    public void PutToPool(IPoolObject obj)
    {
        obj.ResetObject();
        obj.SetActive(false);
        slimePoolList.Enqueue((Slime)obj);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
