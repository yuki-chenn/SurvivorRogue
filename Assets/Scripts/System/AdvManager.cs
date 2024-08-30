using GoogleMobileAds.Api;
using Survivor.Base;
using System;
using UnityEngine;

public class AdvManager : PersistentSingleton<AdvManager>
{
    // 是否初始化完毕
    public bool isInitialized = false;

    // 是否展示banner
    public bool showBanner = false;

    public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Debug.Log("initialize success");
            isInitialized = true;
            InitInterSuccess();
            InitRewardSuccess();
        });
    }

    private void Update()
    {
        if (isInitialized)
        {
            UpdateBanner();
            UpdateInter();
        }
    }


    #region banner

    private BannerView bannerView;
    public int loadBannerSuccessCount = 0;
    public int loadBannerFailCount = 0;
    public float BannerCd = 3f;
    public float bannerTimer;

#if UNITY_ANDROID
    //private string _bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
    private string _bannerAdUnitId = "ca-app-pub-5633392331827463/1781726899";
#elif UNITY_IPHONE
  private string _bannerAdUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _bannerAdUnitId = "unused";
#endif

    private void CreateBannerView()
    {
        if (bannerView != null)
        {
            DestroyBannerView();
        }
        Debug.Log("Creating banner view");
        bannerView = new BannerView(_bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
        bannerView.OnAdClicked += OnClickBanner;
        bannerView.OnBannerAdLoaded += OnLoadBannerSuccess;
        bannerView.OnBannerAdLoadFailed += OnLoadBannerFail;
    }

    private void LoadBannerAd()
    {
        CreateBannerView();

        var adRequest = new AdRequest();

        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
    }

    private void DestroyBannerView()
    {
        if (bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            bannerView.Destroy();
            bannerView = null;
        }
    }

    private bool CanShowBanner()
    {
        return isInitialized && !showBanner;
    }

    private void OnClickBanner()
    {
        Debug.Log("OnClickBanner");
    }

    private void OnLoadBannerSuccess()
    {
        Debug.Log("OnLoadBannerSuccess");
        loadBannerSuccessCount++;
        LogRate();
    }

    private void OnLoadBannerFail(LoadAdError error)
    {
        Debug.Log($"OnLoadBannerSuccess : {error.ToString()}");
        loadBannerFailCount++;
        LogRate();
    }

    private void LogRate()
    {
        var sum = loadBannerFailCount + loadBannerSuccessCount;
        var rate = loadBannerSuccessCount * 100f / sum;
        Debug.Log($"banner填充率：{rate}%");
    }

    private void UpdateBanner()
    {
        if (showBanner)
        {
            bannerTimer -= Time.deltaTime;
            if (bannerTimer <= 0f)
            {
                LoadBannerAd();
                bannerTimer = BannerCd;
            }
        }
        else
        {
            DestroyBannerView();
        }
        
    }

    public void ShowBanner()
    {
        if (!CanShowBanner()) return;
        showBanner = true;
        LoadBannerAd();
    }

    public void CloseBanner()
    {
        showBanner = false;
    }

    #endregion

    #region 插屏广告

#if UNITY_ANDROID
    //private string _interAdUnitId = "ca-app-pub-3940256099942544/1033173712";
    private string _interAdUnitId = "ca-app-pub-5633392331827463/1398583515";
#elif UNITY_IPHONE
  private string _interAdUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _interAdUnitId = "unused";
#endif

    private InterstitialAd interstitialAd;
    public float InterCd = 180f;
    public float interCdTimer;
    public int interLoadSuccessCount;
    public int interLoadFailCount;
    public int interShowSuccessCount;

    private void LoadInter()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        var adRequest = new AdRequest();
        InterstitialAd.Load(_interAdUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    interLoadFailCount++;
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interLoadSuccessCount++;
                interstitialAd = ad;
                interstitialAd.OnAdFullScreenContentClosed += CloseInter;
                interstitialAd.OnAdFullScreenContentFailed += ShowInterFail;
                interstitialAd.OnAdFullScreenContentOpened += ShowInterSuccess;
            });
    }

    public void ShowInter()
    {
        if (!CanShowInter()) return;
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    private bool CanShowInter()
    {
        return isInitialized && interCdTimer <= 0;
    }

    private void InitInterSuccess()
    {
        interCdTimer = 60f;
        LoadInter();
    }

    private void CloseInter()
    {
        LoadInter();
        interCdTimer = InterCd;
    }

    private void ShowInterFail(AdError error)
    {
        Debug.LogError($"error : {error.ToString()}");
        LoadInter();
    }

    private void ShowInterSuccess()
    {
        interShowSuccessCount++;
        LogInterRate();
    }

    private void UpdateInter()
    {
        if (interCdTimer > 0)
        {
            interCdTimer -= Time.deltaTime;
        }
    }

    private void LogInterRate()
    {
        var loadTotal = interLoadSuccessCount + interLoadFailCount;
        //填充率： 加载成功的次数 / 加载的总次数
        var rate = interLoadSuccessCount * 100f / loadTotal;
        Debug.Log($"inter填充率 : {rate}%");
        //展示率： 展示成功的次数 / 加载的总次数
        rate = interShowSuccessCount * 100f / loadTotal;
        Debug.Log($"inter展示率 : {rate}%");
    }

    #endregion

    #region 激励广告
#if UNITY_ANDROID
    //private string _rewardAdUnitId = "ca-app-pub-3940256099942544/5224354917";
    private string _rewardAdUnitId = "ca-app-pub-5633392331827463/5232154808";
#elif UNITY_IPHONE
  private string _rewardAdUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string _rewardAdUnitId = "unused";
#endif

    private RewardedAd rewardedAd;

    private void InitRewardSuccess()
    {
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        var adRequest = new AdRequest();

        RewardedAd.Load(_rewardAdUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                rewardedAd.OnAdFullScreenContentClosed += CloseReward;
                rewardedAd.OnAdFullScreenContentFailed += ShowRewardFail;
            });
    }

    private void ShowRewardFail(AdError error)
    {
        Debug.Log($"error : {error.ToString()}");
        LoadRewardedAd();
    }

    private void CloseReward()
    {
        Debug.Log("Close Reward");
        LoadRewardedAd();
    }

    public void ShowReward(Action callback)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                callback?.Invoke();
            });
        }
    }

    #endregion

}
