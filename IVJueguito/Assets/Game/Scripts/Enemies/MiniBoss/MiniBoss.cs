using UnityEngine;

public class MiniBoss : Enemy
{
    [HideInInspector] public bool triggerSpecial = false;
    [HideInInspector] public bool specialDone = false;
    [HideInInspector] public bool activateParticles = true;
    [SerializeField] ParticleSystem windArea;
    [SerializeField] ParticleSystem stunned;
    public ParticleSystem explosion;
        public MiniBossFlyweight MiniBossData
        {
            get { return (MiniBossFlyweight)flyweightData; }
        }

        public void pushPlayer(float force, float radius)
        {
            if(activateParticles)
            {
                windArea.Play();
                activateParticles = false;
            }

            GameObject player = GameObject.FindWithTag("Player");
            if (DistanceWithPlayer() < radius)
            {

            Debug.Log("Estoy entrando al emuje");
                    Vector3 dir = (player.transform.position - transform.position).normalized;
                    dir.y = 0;

                    Rigidbody rb = player.GetComponent<Rigidbody>();


            player.transform.Translate(dir * force * Time.deltaTime, Space.World);
            //rb.linearVelocity += dir * force * Time.deltaTime;
            //rb.AddForce(dir * force, ForceMode.Force);
        }
    }

        public void StunEffect()
            {
                stunned.Play();
            }

        public void SpecialAttack()
        {
            explosion.Play();
        }

    protected override void Update()
    {
        base.Update();

        if (currentHp < 5 && specialDone == false)
        {
            triggerSpecial = true;
            specialDone = true;
        }

    }

}
