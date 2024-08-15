public enum EventDefine
{
    // 显示隐藏界面
    ShowMainMenuPanel,
    HideMainMenuPanel,
    ShowSelectPanel,
    HideSelectPanel,
    ShowWeaponItemPanel,
    HideWeaponItemPanel,
    ShowShopPanel,
    HideShopPanel,
    ShowWeaponPopUpPanel,
    HideWeaponPopUpPanel,
    ShowOpenChestPanel,
    HideOpenChestPanel,
    ShowGameOverPanel,
    ShowGameClearPanel,
    HideGameClearPanel,
    ShowDataFilePanel,
    HideDataFilePanel,
    ShowPausePanel,
    HidePausePanel,

    // 更新初始选择的武器
    RefreshSelectWeapon,
    // 更新初始选择的道具
    RefreshSelectItem,
    // 卖武器后更新UI
    OnWeaponSaled,
    // 升级武器后更新UI
    OnWeaponRanked,
    // 开宝箱选择后
    OnChestItemSelect,
}
