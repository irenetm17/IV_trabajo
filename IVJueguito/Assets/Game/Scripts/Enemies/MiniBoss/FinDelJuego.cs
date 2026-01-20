using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FinDelJuego : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject textoFin;
    [SerializeField] private GameObject botonFinal;

    [Header("Tiempos")]
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float delayTexto = 0.5f;
    [SerializeField] private float delayObjetoFinal = 1.0f;

    private bool _hasGameEnded = false;

    private void Awake()
    {
        textoFin.SetActive(false);
        botonFinal.SetActive(false);

        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;
    }

    private void Update()
    {
        if (_hasGameEnded)
        {
            if(Mouse.current.leftButton.wasPressedThisFrame)
            {
                IrMenuPrincipal();
            }
        }
    }

    public void EndGame()
    {
        StartCoroutine(FinDelJuegoCoroutine());
    }

    private IEnumerator FinDelJuegoCoroutine()
    {
        Debug.Log("CORRUTINA Fin del juego");
        float t = 0f;
        Color c = fadeImage.color;
        fadeImage.gameObject.SetActive(true);

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;

        yield return new WaitForSeconds(delayTexto);
        textoFin.SetActive(true);

        yield return new WaitForSeconds(delayObjetoFinal);
        botonFinal.SetActive(true);

        _hasGameEnded = true;
    }

    public void IrMenuPrincipal()
    {
        Time.timeScale = 1f;
        AudioService.instance.PlaySFX("Boton");
        SceneManager.LoadScene("MenuInicial");
    }
}