using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesInicio : MonoBehaviour
{
    [SerializeField] private GameObject panelCreditos;
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject menuSeleccion;

    private void Start()
    {
        Time.timeScale = 1f;
        if (panelCreditos != null)
            panelCreditos.SetActive(false);
        if(menuInicial != null)
            menuInicial.SetActive(true);
        if(menuSeleccion != null)
            menuSeleccion.SetActive(false);
    }
    public void IniciarJuego()
    {
        AudioService.instance.PlaySFX("Boton");
        Time.timeScale = 1f;
        if (menuSeleccion != null)
            menuSeleccion.SetActive(true);
        if (menuInicial != null)
            menuInicial.SetActive(false);
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
        if (menuSeleccion != null)
            menuSeleccion.SetActive(true);
    }
    public void IrMenuPrincipal()
    {
        Time.timeScale = 1f;
        AudioService.instance.PlaySFX("Boton");
        SceneManager.LoadScene("MenuInicial");
    }
}
