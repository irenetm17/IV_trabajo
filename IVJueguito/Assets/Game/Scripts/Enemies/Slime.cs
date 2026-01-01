using UnityEngine;

public class Slime : /*Enemy, */MonoBehaviour, IPoolObject  // Tambien implementará el Enemy
{
    public SlimePool parentSlimePool;

    public bool isActive()
    {
        throw new System.NotImplementedException();
    }

    public void MoveTo(Vector3 position)
    {
        this.transform.position = position;
    }

    public void ResetObject()
    {
        //Variables como health, reset


        //Mover al origen
        this.MoveTo(Vector3.zero);
    }

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.SetActive(false);
            parentSlimePool.PutToPool(this);
        }
    }

}
