using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdBanner : MonoBehaviour
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    [SerializeField] float _delayBetweenLoadChecks = 1f;

    private string _adUnitId = "Banner Android";

    private void Start()
    {
        Advertisement.Banner.SetPosition(_bannerPosition);
    }

    public void Show()
    {
        StartCoroutine(ShowWhenReady());
    }

    public void Hide()
    {
        Advertisement.Banner.Hide();
    }

    private IEnumerator ShowWhenReady()
    {
        while (!Advertisement.Banner.isLoaded)
        {
            yield return new WaitForSecondsRealtime(_delayBetweenLoadChecks);

            Advertisement.Banner.Load(_adUnitId);
        }

        Advertisement.Banner.Show(_adUnitId);
    }
}
