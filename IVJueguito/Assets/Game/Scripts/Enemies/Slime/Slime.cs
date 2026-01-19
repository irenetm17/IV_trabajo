using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Slime : Enemy, IPoolObject  // Tambien implementará el Enemy
{
    public SlimePool parentSlimePool;

    [SerializeField] private GameObject _heartGO;


    public override void KillEnemy()
    {

        AudioService.instance.PlaySFX("MuerteEnemigo");

        int randomHeart = Random.Range(1,6);
        Debug.Log("Heart Num: "+randomHeart);

        if(randomHeart == 3)
        {
            Debug.Log("Heart Spawn");
            Instantiate(_heartGO,transform.position,Quaternion.identity);
        }

        // Si tenemos una pool asignada, volvemos a ella
        if (parentSlimePool != null)
        {
            ChangeState(flyweightData.idleState);
            SetActive(false);
            parentSlimePool.PutToPool(this);
        }
    }

    public bool isActive()
    {
        throw new System.NotImplementedException();
    }

    public void DisplaceTo(Vector3 position)
    {
        this.transform.position = position;
    }

    public void ResetObject()
    {
        transform.rotation = Quaternion.identity; 

        if (animator != null)
        {
            animator.Rebind(); 
            animator.Update(0f);
        }
       
        transform.localScale = Vector3.one;
        Initialize(tipoParaTest);

        //Mover al origen
        this.DisplaceTo(Vector3.zero);
    }

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //this.SetActive(false);
            //parentSlimePool.PutToPool(this);
            AudioService.instance.PlaySFX("Mordisco");
        }
    }

}
