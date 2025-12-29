using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePool : IObjectPool
{
    [SerializeField]
    private int initialSize = 40;
    private Queue<Slime> slimePoolList;

    public void Awake()
    {
        slimePoolList = new Queue<Slime>(initialSize);
    }



    public void PutToPool(IObjectPool obj)
    {
        throw new System.NotImplementedException();
    }

    public void StartPool()
    {
        throw new System.NotImplementedException();
    }

    public IPoolObject TakeFromPool()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
