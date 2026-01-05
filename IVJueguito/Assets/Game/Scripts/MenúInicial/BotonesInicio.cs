using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesInicio : MonoBehaviour
{
    [SerializeField] private GameObject panelCreditos;
    [SerializeField] private GameObject menuInicial;
    public void IniciarJuego()
    {
        SceneManager.LoadScene("Game Scene");
        EventManager.instance.Publicar(new SimpleEvent(eventType.LevelStarted));
    }
    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }

    public void AbrirCreditos()
    {
        panelCreditos.SetActive(true);
        menuInicial.SetActive(false);
    }
    public void CerrarCreditos()
    {
        panelCreditos.SetActive(false);
        menuInicial.SetActive(true);
    }
}
