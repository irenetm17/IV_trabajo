using System.Collections;
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

    [SerializeField]
    private Light _light;
    private float minIntensity = 0f;
    [SerializeField]
    private float maxIntensity = 300f;
    [SerializeField]
    private float _pulseDuration = 2f; // tiempo de subida (y bajada)


    private void Start()
    {
        if (_light != null)
        {
            _light.intensity = minIntensity;
            StartCoroutine(LightPulseCoroutine());
        }
    }



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


    private IEnumerator LightPulseCoroutine()
    {
        while (true)
        {
            // Subida int

            float t = 0f;
            while (t < _pulseDuration)
            {
                t += Time.deltaTime;
                float lerp = t / _pulseDuration;
                _light.intensity = Mathf.Lerp(minIntensity, maxIntensity, lerp);
                yield return null;
            }

            // Bajada int
            t = 0f;
            while (t < _pulseDuration)
            {
                t += Time.deltaTime;
                float lerp = t / _pulseDuration;
                _light.intensity = Mathf.Lerp(maxIntensity, minIntensity, lerp);
                yield return null;
            }
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
