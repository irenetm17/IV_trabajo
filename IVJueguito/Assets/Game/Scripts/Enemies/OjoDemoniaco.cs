using UnityEngine;

public class OjoDemoniaco : MonoBehaviour
{
    [SerializeField]
    private GameObject _globoOcular;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Transform _bulletT;

    [SerializeField]
    private GameObject _bulletGO;

    private Vector3 vectorOjoPlayer;
    private float _timer = 5;
    [SerializeField]
    private float _maxShootTime = 5;

    [SerializeField]
    private float _speedShot = 5f;

    [SerializeField]
    private float _shootingDistance = 30f;



    private void Update()
    {
        vectorOjoPlayer = _player.transform.position - _globoOcular.transform.position;
        vectorOjoPlayer = vectorOjoPlayer.normalized;

        _globoOcular.transform.up = -vectorOjoPlayer;

        _timer += Time.deltaTime;

        if(Vector3.Distance(_player.transform.position, _globoOcular.transform.position) < _shootingDistance)
        {
            if (_timer < _maxShootTime) return;

            GameObject bulletTemp = Instantiate(_bulletGO, _bulletT.position, Quaternion.identity);

            Vector3 dir = _player.transform.position - _bulletT.position;
            dir = dir.normalized;

            Proyectile proyectil = bulletTemp.GetComponent<Proyectile>();
            proyectil.Init(dir, _speedShot);

            Destroy(bulletTemp, 10f);
            _timer = 0f;
        }

    }
    



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RayoLaser"))
        {
            Debug.Log("Rayo Laser");
            Destroy(gameObject);
            Destroy(collision.gameObject); //Destruir rayo

            if(transform.parent.gameObject.GetComponent<MuroOjosEsmeralda>() != null)
            {
                transform.parent.gameObject.GetComponent<MuroOjosEsmeralda>().DestrozarMuro();
            }

            //if(scriptPuerta){ abrir puerta }

        }
    }


}
