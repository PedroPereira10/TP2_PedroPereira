using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration = 1f;
    private bool _isPlayerOnPlatform;

    private void Start()
    {
        if (_fadeImage != null)
        {
            _fadeImage.color = new Color(0, 0, 0, 0);
            _fadeImage.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            _isPlayerOnPlatform = true;
            StartCoroutine(FadeAndLoadScene());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            _isPlayerOnPlatform = false;
            StopCoroutine(FadeAndLoadScene());
            ResetFade();
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        if (_fadeImage != null)
        {
            _fadeImage.gameObject.SetActive(true);

            // Optional delay before starting the fade
            yield return new WaitForSeconds(0.5f);

            if (_isPlayerOnPlatform)
            {
                float elapsed = 0f;

                // Fade out to black
                while (elapsed < _fadeDuration)
                {
                    elapsed += Time.deltaTime;
                    float alpha = Mathf.Clamp01(elapsed / _fadeDuration);
                    _fadeImage.color = new Color(0, 0, 0, alpha); // Fade to black
                    yield return null;
                }

                SceneManager.LoadScene(_sceneToLoad);
            }
        }
    }

    private void ResetFade()
    {
        if (_fadeImage != null)
        {
            _fadeImage.color = new Color(0, 0, 0, 0);
            _fadeImage.gameObject.SetActive(false);
        }
    }
}
