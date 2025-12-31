using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField]
    private SlimePool _slimePool;
    [SerializeField]
    private Transform[] _slimeSpawnersT;

    [SerializeField]
    private GameObject _slimeTEMP;

    [SerializeField]
    private int _maxSlimesSpawn = 5;
    [SerializeField]
    private int _minSlimesSpawn = 2;

    private Transform _player;

    // CONTROL DEL SPAWN
    [SerializeField]
    private float _spawnDistanceFromPlayer = 20;

    [SerializeField]
    private float _minRandomSpreadSpawn = 1;
    [SerializeField]
    private float _maxRandomSpreadSpawn = 15;


    private float _spawnTimer = 30;
    [SerializeField]
    private float _spawnTime = 30;


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        CheckSpawns();
    }

    private void CheckSpawns()
    {
        if (_spawnTimer > _spawnTime)
        {
            for (int i = 0; i < _slimeSpawnersT.Length; i++)
            {

                float dist = Vector3.Distance(_player.position, _slimeSpawnersT[i].position);
                if (dist <= _spawnDistanceFromPlayer)
                {
                    SpawnSlimes(_slimeSpawnersT[i]);
                    _spawnTimer = 0;
                    break;
                }
            }
        }
    }

    void SpawnSlimes(Transform spawnPos)
    {
        int numSlimesSpawn = Random.Range(_minSlimesSpawn,_maxSlimesSpawn); // Se calcula un numero random de spawn de slimes


        for (int i = 0; i < numSlimesSpawn; i++)
        {
            Vector3 randomSpreadSpawn = new Vector3(
                Random.Range(_minRandomSpreadSpawn, _maxRandomSpreadSpawn),0,Random.Range(_minRandomSpreadSpawn, _maxRandomSpreadSpawn));

            Vector3 spawnPosition = spawnPos.position + randomSpreadSpawn;

            //Instantiate(_slimeTEMP, spawnPosition, Quaternion.identity);

            IPoolObject slimePooled = _slimePool.TakeFromPool();
            slimePooled.ResetObject();
            slimePooled.MoveTo(spawnPosition);


            Debug.Log("Slime spawned at pos: " + spawnPosition);
        }

        //Llamar a la pool y resetear, activar y mover el slime de la pool al randomSpreadSpawn (de momento no)

    }

}
