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
            _fadeImage.color = new Color(0, 0, 0, 1);
            _fadeImage.gameObject.SetActive(false);
            StartCoroutine(FadeOut());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            _isPlayerOnPlatform = true;
            StartCoroutine(FadeInAndLoadScene());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            _isPlayerOnPlatform = false;
            StopCoroutine(FadeInAndLoadScene());
            ResetFade();
        }
    }

    private IEnumerator FadeInAndLoadScene()
    {
        if (_fadeImage != null)
        {
            _fadeImage.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            if (_isPlayerOnPlatform)
            {
                float elapsed = 0f;

                while (elapsed < _fadeDuration)
                {
                    elapsed += Time.deltaTime;
                    float alpha = Mathf.Clamp01(elapsed / _fadeDuration);
                    _fadeImage.color = new Color(0, 0, 0, alpha); 
                    yield return null;
                }

                SceneManager.LoadScene(_sceneToLoad);
            }
        }
    }

    private IEnumerator FadeOut()
    {
        if (_fadeImage != null)
        {
            _fadeImage.gameObject.SetActive(true);
            float elapsed = 0f;

            _fadeImage.color = new Color(0, 0, 0, 1);

            while (elapsed < _fadeDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(1, 0, elapsed / _fadeDuration); 
                _fadeImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            _fadeImage.gameObject.SetActive(false);
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
