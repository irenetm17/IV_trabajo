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
        AudioService.instance.PlaySFX("Boton");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game Scene");
        EventManager.instance.Publicar(new SimpleEvent(eventType.LevelStarted));
    }
    public void SalirJuego()
    {
        AudioService.instance.PlaySFX("Boton");
        Application.Quit();
        Debug.Log("Salir del juego");
    }

    public void AbrirCreditos()
    {
        AudioService.instance.PlaySFX("Boton");
        panelCreditos.SetActive(true);
        menuInicial.SetActive(false);
    }
    public void CerrarCreditos()
    {
        AudioService.instance.PlaySFX("Boton");
        panelCreditos.SetActive(false);
        menuInicial.SetActive(true);
    }
    public void IrMenuPrincipal()
    {
        Time.timeScale = 1f;
        AudioService.instance.PlaySFX("Boton");
        SceneManager.LoadScene("MenuInicial");
    }
}
