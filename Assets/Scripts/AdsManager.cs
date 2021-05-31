using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;

using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{
    private string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    private BannerView bannerView;

    private string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    private RewardedAd rewardedAd;
    
    private bool rewarded = false;
    public Text tmp;

    private void Awake()
  {            
    Debug.LogError("Awake");

    DontDestroyOnLoad(gameObject);

    // Initialize the Google Mobile Ads SDK.
    MobileAds.Initialize(initStatus => { });
  }

    // Start is called before the first frame update
    void Start()
    {
        /*
        string id = bannerTestID;
        bannerView = new BannerView(id, AdSize.MediumRectangle, AdPosition.Center);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        bannerView.Hide();
*/
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd = new RewardedAd(rewardTestID);
        rewardedAd.LoadAd(request); // 광고 로드
 
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // 사용자가 광고를 끝까지 시청했을 때
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        // 사용자가 광고를 중간에 닫았을 때

        UserChoseToWatchAd();
    }

    // Update is called once per frame
    void Update()
    {
        if (rewarded)
        {
        }
    }

    public void UserChoseToWatchAd()
    {
        if (rewardedAd.IsLoaded()) // 광고가 로드 되었을 때
        {
            rewardedAd.Show(); // 광고 보여주기
        }
    }
 
    public void CreateAndLoadRewardedAd() // 광고 다시 로드하는 함수
    {
        rewardedAd = new RewardedAd(rewardTestID);
 
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
 
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }
 
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {  // 사용자가 광고를 닫았을 때
        CreateAndLoadRewardedAd();  // 광고 다시 로드
    }
 
    private void HandleUserEarnedReward(object sender, Reward e)
    {  // 광고를 다 봤을 때
        rewarded = true; // 변수 true

            tmp.text = "리워드 획득!!!";
            rewarded = false;

    }
}
