using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyweightFactory : MonoBehaviour
{
    public static EnemyFlyweightFactory Instance { get; private set; }

    private Dictionary<EnemyType, EnemyFlyweight> flyweightDictionary;

    [SerializeField] private List<EnemyFlyweight> flyweights;

    void Awake()
    {
      if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        flyweightDictionary = new Dictionary<EnemyType, EnemyFlyweight>();

        foreach (var flyweight in flyweights)
        {
            if (!flyweightDictionary.ContainsKey(flyweight.typeID))
            {
                flyweightDictionary.Add(flyweight.typeID, flyweight);
            }
        }
    }
    public EnemyFlyweight GetFlyweight(EnemyType type)
    {
        if (flyweightDictionary.TryGetValue(type, out EnemyFlyweight data))
        {
            return data;
        }
        else
        {
            return null;
        }
    }


}

