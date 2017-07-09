﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Protocol;

public class UIEquipInfo : GTWindow
{
    public UIEquipInfo()
    {
        mResident = false;
        mResPath = "Public/UIEquipInfo";
        Type = EWindowType.DIALOG;
    }

    protected override void OnAwake()
    {
        Transform left = transform.Find("Left");
        Transform right = transform.Find("Right");
        Transform buttons = transform.Find("Buttons");
        equipName = left.Find("EquipName").GetComponent<UILabel>();
        equipStep = left.Find("Step").GetComponent<UILabel>();
        equipSuit = left.Find("Suit").GetComponent<UILabel>();
        equipFightValue = left.Find("FightValue").GetComponent<UILabel>();
        equipAdvanceLevel = left.Find("Item/AdvanceLevel").GetComponent<UILabel>();
        equipEquipType = left.Find("EquipType").GetComponent<UILabel>();
        equipQuality = left.Find("Item/Quality").GetComponent<UISprite>();
        equipTexture = left.Find("Item/Texture").GetComponent<UITexture>();
        equipDressPic = left.Find("Item/Dress").gameObject;
        equipPropertys = left.Find("View/Propertys").GetComponent<UILabel>();

        btnClose = buttons.Find("Btn_Close").gameObject;
        btnDress = buttons.Find("Btn_Dress").gameObject;
        btnSell = buttons.Find("Btn_Sell").gameObject;
        btnUnload = buttons.Find("Btn_Unload").gameObject;
        btnUp = buttons.Find("Btn_Up").gameObject;

        view  = right.Find("View").GetComponent<UIScrollView>(); ;
        text1 = right.Find("View/Text1").GetComponent<UILabel>();
        text2 = right.Find("View/Text2").GetComponent<UILabel>();
        text3 = right.Find("View/Text3").GetComponent<UILabel>();

 

        title1 = right.Find("Title_1").GetComponent<UILabel>();
        title2 = right.Find("Title_2").GetComponent<UILabel>();
        title3 = right.Find("Title_3").GetComponent<UILabel>();
        menuBtn1 = right.Find("Menu_1").GetComponent<UIToggle>();
        menuBtn2 = right.Find("Menu_2").GetComponent<UIToggle>();
        menuBtn3 = right.Find("Menu_3").GetComponent<UIToggle>();

    }

    protected override void OnAddButtonListener()
    {
        UIEventListener.Get(btnClose).onClick = OnCloseClick;
        UIEventListener.Get(btnUnload).onClick = OnUnloadlClick;
        UIEventListener.Get(btnSell).onClick = OnSellClick;
        UIEventListener.Get(btnDress).onClick = OnDressClick;
        UIEventListener.Get(btnUp).onClick = OnUpClick;
        UIEventListener.Get(menuBtn3.gameObject).onClick = OnRightMenuClick;
        UIEventListener.Get(menuBtn2.gameObject).onClick = OnRightMenuClick;
        UIEventListener.Get(menuBtn1.gameObject).onClick = OnRightMenuClick;
    }

    private void OnRightMenuClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        if (mCurMenuBtnName == go.name)
            return;
        view.ResetPosition();
        mCurMenuBtnName = go.name;
    }

    protected override void OnAddHandler()
    {
        GTEventCenter.AddHandler<int,int>(GTEventID.TYPE_DRESS_EQUIP,  OnRecvDressEquip);
        GTEventCenter.AddHandler<int,int>(GTEventID.TYPE_UNLOAD_EQUIP, OnRecvUnloadEquip);
    }

    protected override void OnDelHandler()
    {
        GTEventCenter.DelHandler<int,int>(GTEventID.TYPE_DRESS_EQUIP,  OnRecvDressEquip);
        GTEventCenter.DelHandler<int,int>(GTEventID.TYPE_UNLOAD_EQUIP, OnRecvUnloadEquip);
    }

    private void OnUpClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        GTItemHelper.ShowEquipWindowByPos(mPosType, mPos);
        Hide();
    }

    private void OnDressClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        BagService.Instance.TryDressEquip(mPos);
    }

    private void OnSellClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
    }

    private void OnUnloadlClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLICK);
        BagService.Instance.TryUnloadEquip(mPos);
    }

    private void OnCloseClick(GameObject go)
    {
        GTAudioManager.Instance.PlayEffectAudio(GTAudioKey.SOUND_UI_CLOSE);
        Hide();
    }

    private void InitToggleView()
    {
        int group = GTWindowManager.Instance.GetToggleGroupId();
        menuBtn2.group = group;
        menuBtn3.group = group;
        menuBtn1.group = group;
        menuBtn1.value = true;
        mCurMenuBtnName = menuBtn1.name;
    }

    private void ShowDress(bool isDress)
    {
        btnUnload.SetActive(isDress);
        equipDressPic.SetActive(isDress);
        btnDress.SetActive(!isDress);
        btnSell.SetActive(!isDress);
    }


    public void ShowInfoById(int itemID)
    {

    }

    public void ShowInfoByPos(EPosType posType, int pos)
    {
        this.mPos = pos;
        this.mPosType = posType;
        ShowDress(posType == EPosType.RoleEquip);
        XEquip equip = GTDataManager.Instance.GetEquipDataByPos(mPosType, mPos);
        if (equip == null) return;
        int itemID = equip.Id;
        ShowBaseView(itemID);
        equipFightValue.text = GTTools.Format("战斗力 {0}",AttrHelper.GetFightValue(equip));
        equipAdvanceLevel.text = EquipModule.Instance.GetEquipAdvanceNameByLevel(equip.AdvanceLevel);
        Dictionary<EAttr, int> propertys = AttrHelper.GetPropertys(equip);
        title3.text = GTTools.Format("装备星级 {0}", equip.StarLevel);
        title2.text = GTTools.Format("进阶等级 {0}", equip.AdvanceLevel);
        title1.text = GTTools.Format("强化等级 {0}", equip.StrengthenLevel);
        GTItemHelper.ShowPropertyText(this.equipPropertys, propertys, true);
        GTItemHelper.ShowEquipStrengthText(text1, itemID, equip.StrengthenLevel);
        GTItemHelper.ShowEquipAdvanceText(text2, itemID, equip.AdvanceLevel);
        GTItemHelper.ShowEquipStarText(text3, itemID, equip.StarLevel);
    }

    private void ShowBaseView(int itemID)
    {
        equipStep.text = EquipModule.Instance.GetEquipStepByID(itemID);
        equipSuit.text = EquipModule.Instance.GetEquipSuitNameByID(itemID);
        equipEquipType.text = EquipModule.Instance.GetEquipTypeNameByID(itemID);
        GTItemHelper.ShowItemTexture(equipTexture, itemID);
        GTItemHelper.ShowItemQuality(equipQuality, itemID);
        GTItemHelper.ShowItemName(equipName, itemID);
    }

    private void OnRecvUnloadEquip(int arg1, int arg2)
    {
        Hide();
    }

    private void OnRecvDressEquip(int arg1, int arg2)
    {
        Hide();
    }

    protected override void OnClose()
    {
        mCurMenuBtnName = string.Empty;
    }

    protected override void OnEnable()
    {
        InitToggleView();
    }

    private UILabel equipName;
    private UILabel equipStep;
    private UILabel equipSuit;
    private UILabel equipFightValue;
    private UILabel equipAdvanceLevel;
    private UITexture equipTexture;
    private UISprite equipQuality;
    private GameObject equipDressPic;

    private GameObject btnClose;
    private GameObject btnUnload;
    private GameObject btnDress;
    private GameObject btnUp;
    private GameObject btnSell;

    private UILabel equipEquipType;


    private UILabel title2;
    private UILabel title3;
    private UILabel title1;

    private UIToggle menuBtn1;
    private UIToggle menuBtn2;
    private UIToggle menuBtn3;

    private UIScrollView view;
    private UILabel text1;
    private UILabel text2;
    private UILabel text3;
    private UILabel equipPropertys;

    private int mPos;
    private EPosType mPosType;
    private string mCurMenuBtnName;
}
