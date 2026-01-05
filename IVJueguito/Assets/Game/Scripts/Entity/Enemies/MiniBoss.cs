using UnityEngine;

public class MiniBoss : Enemy
{
    [HideInInspector] public bool triggerSpecial = false;
    [HideInInspector] public bool specialDone = false;
        public MiniBossFlyweight MiniBossData
        {
            get { return (MiniBossFlyweight)flyweightData; }
        }

        public void pushPlayer(float force, float radius)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (DistanceWithPlayer() < radius)
            {
                    Vector3 dir = (player.transform.position - transform.position).normalized;
                    dir.y = 0;

                    Rigidbody rb = player.GetComponent<Rigidbody>();
                    rb.AddForce(dir * force * Time.deltaTime * 50f, ForceMode.Force);
            }
        }
    protected override void Update()
    {
        base.Update();

        if (currentHp < 500 && specialDone == false)
        {
            triggerSpecial = true;
            specialDone = true;
        }

    }

}
