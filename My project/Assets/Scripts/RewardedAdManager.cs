using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewardedAdManager : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private string adUnitId = "ca-app-pub-2084966746848693/5246994800"; // Replace with your real AdMob Rewarded Ad Unit ID

    public GameManager gameManager; // Reference to GameManager

    void Start()
    {
        MobileAds.Initialize(initStatus => { LoadRewardedAd(); });
    }

    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading rewarded ad...");

        RewardedAd.Load(adUnitId, new AdRequest(), (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Failed to load rewarded ad: " + error.GetMessage());
                return;
            }

            rewardedAd = ad;
            Debug.Log("Rewarded ad loaded.");

            // Register event handlers
            rewardedAd.OnAdFullScreenContentClosed += HandleAdClosed;
            rewardedAd.OnAdFullScreenContentFailed += HandleAdFailedToShow;
        });
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show(HandleUserEarnedReward);
        }
        else
        {
            Debug.Log("Rewarded ad is not ready.");
            gameManager.ShowGameOver(); // If ad is not ready, show game over
        }
    }

    private void HandleUserEarnedReward(Reward reward)
    {
        Debug.Log("Player watched ad! Granting extra chance.");
        gameManager.RevivePlayer();
    }

    private void HandleAdClosed()
    {
        Debug.Log("Ad closed. Reloading new ad...");
        LoadRewardedAd(); // Load new ad for next time
    }

    private void HandleAdFailedToShow(AdError error)
    {
        Debug.LogError("Rewarded ad failed to show: " + error.GetMessage());
        gameManager.ShowGameOver(); // If ad fails, go to game over
    }
}
