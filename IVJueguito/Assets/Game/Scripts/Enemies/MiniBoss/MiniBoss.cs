using UnityEngine;

public class MiniBoss : Enemy
{
    [HideInInspector] public bool triggerSpecial = false;
    [HideInInspector] public bool special1Done = false;
    [HideInInspector] public bool special2Done = false;
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
                Debug.Log("Efecto de viento encendido");
            }

            GameObject player = GameObject.FindWithTag("Player");
            if (DistanceWithPlayer() < radius)
            {

            Debug.Log("Estoy entrando al empuje");
                    Vector3 dir = (player.transform.position - transform.position).normalized;
                    dir.y = 0;
                AudioService.instance.PlaySFX("GolpeGolem");

            Rigidbody rb = player.GetComponent<Rigidbody>();


            //player.transform.Translate(dir * force * Time.deltaTime, Space.World);
            rb.linearVelocity += dir * force * Time.deltaTime;
            rb.AddForce(dir * force, ForceMode.Force);
        }
    }

    public void StopWind()
    {
        windArea.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        activateParticles = true;
        Debug.Log("Efecto apagado");
    }
    public void StunEffect()
        {

            stunned.Play();
        
            Debug.Log("Efecto de stun encendido");
        }

        public void SpecialAttack()
        {
        
            explosion.Play();
        
            Debug.Log("Efecto de explosión encendido");

        }

    protected override void Update()
    {
        base.Update();

        if (currentHp <= 15 && special1Done == false)
        {
            this.ChangeState(this.MiniBossData.specialGimmickBossState);
            special1Done = true;
        }

        if (currentHp <= 5 && special2Done == false)
        {
            this.ChangeState(this.MiniBossData.specialGimmickBossState);
            special2Done = true;
        }

    }

}
