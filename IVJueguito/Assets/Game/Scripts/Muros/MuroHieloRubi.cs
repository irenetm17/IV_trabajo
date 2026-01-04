using UnityEngine;

public class MuroHieloRubi : MonoBehaviour
{
    [SerializeField]
    private Animator _animatorMuro;

    private void Start()
    {
        _animatorMuro = GetComponent<Animator>();
        _animatorMuro.enabled = false;
    }


    public void DerretirMuro()
    {
        _animatorMuro.enabled = true;
        Destroy(gameObject,3f);
    }

}
