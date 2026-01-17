using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

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
            // Estado
            slimeTemp.ChangeState(slimeTemp.flyweightData.idleState);
            slimeTemp.isAlive = true;

            slimeTemp.SetActive(false);
            _slimePoolList.Enqueue(slimeTemp);
        }
    }


    public IPoolObject TakeFromPool()
    {
        if( _slimePoolList.Count == 0) //generar nuevos
        {
            GameObject slimeTempGO = Instantiate(_slimePrefab);
            Slime slimeTemp = slimeTempGO.GetComponent<Slime>();
            slimeTemp.parentSlimePool = this;
            slimeTemp.SetActive(true);
            slimeTemp.ResetObject();
            _actualSize++;
            return slimeTemp;
        }
        else //Pillarlo de la pool
        {
            Slime slimeTemp = _slimePoolList.Dequeue();
            slimeTemp.parentSlimePool = this;
            slimeTemp.SetActive(true);
            slimeTemp.ResetObject();
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
