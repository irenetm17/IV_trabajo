using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePool : MonoBehaviour, IObjectPool
{
    [SerializeField]
    private int _initialSize = 40;
    [SerializeField]
    private int _actualSize;
    private Queue<Slime> _slimePoolList;

    [SerializeField]
    private GameObject _slimePrefab;

    public void Awake()
    {
        _actualSize = _initialSize;
        _slimePoolList = new Queue<Slime>(_initialSize);
        StartPool();
    }


    public void StartPool()
    {
        for (int i = 0; i<_initialSize ; i++ )
        {
            GameObject slimeTempGO = Instantiate(_slimePrefab);
            Slime slimeTemp = slimeTempGO.GetComponent<Slime>();
            slimeTemp.parentSlimePool = this;
            slimeTemp.ResetObject();
            slimeTemp.SetActive(false);
            _slimePoolList.Enqueue(slimeTemp);
        }
    }


    public IPoolObject TakeFromPool()
    {
        if( _slimePoolList.Count == 0)
        {
            GameObject slimeTempGO = Instantiate(_slimePrefab);
            Slime slimeTemp = slimeTempGO.GetComponent<Slime>();
            slimeTemp.parentSlimePool = this;
            slimeTemp.ResetObject();
            slimeTemp.SetActive(true);
            _actualSize++;
            return slimeTemp;
        }
        else
        {
            Slime slimeTemp = _slimePoolList.Dequeue();
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
        _slimePoolList.Enqueue((Slime)obj);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
