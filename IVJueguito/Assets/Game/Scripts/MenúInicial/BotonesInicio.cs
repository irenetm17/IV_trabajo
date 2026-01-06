using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesInicio : MonoBehaviour
{
    [SerializeField] private GameObject panelCreditos;
    [SerializeField] private GameObject menuInicial;

    private void Start()
    {
        if(panelCreditos != null)
            panelCreditos.SetActive(false);
        if(menuInicial != null)
            menuInicial.SetActive(true);
    }
    public void IniciarJuego()
    {
        Time.timeScale = 1f;
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
    public void IrMenuPrincipal()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}
