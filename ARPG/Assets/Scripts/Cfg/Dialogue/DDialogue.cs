﻿using UnityEngine;
using System.Collections;
using System;
using System.Xml;

public enum EDialoguePos
{
    LF           = 0,
    RT           = 1
}

public enum EDialogueContentShowType
{
    Normal       = 0,
    Effect       = 1
}

public enum EDialogueContentType
{
    TYPE_NONE    = 0,
    TYPE_PLAYER  = 1,
    TYPE_ACTOR   = 2,
    TYPE_ITEM    = 3,
    TYPE_MAP     = 4
}


public class DDialogue : DObj<int>
{
    public int                      Id;
    public int                      Actor;
    public string                   Voice            = string.Empty;
    public EDialoguePos             Pos              = EDialoguePos.LF;
    public string                   Anim             = string.Empty;
    public string                   Content          = string.Empty;
    public EDialogueContentShowType ContentShowType  = EDialogueContentShowType.Effect;
    public EDialogueContentType     ContentType      = EDialogueContentType.TYPE_PLAYER;
    public int                      ContentID;

    public override int GetKey()
    {
        return Id;
    }

    public override void Read(XmlElement element)
    {
        this.Id                = element.GetInt32("Id");
        this.Actor             = element.GetInt32("Actor");
        this.Voice             = element.GetString("Voice");
        this.Pos               = (EDialoguePos)element.GetInt32("Pos");
        this.Anim              = element.GetString("Anim");
        this.Content           = element.GetString("Content");
        this.ContentShowType   = (EDialogueContentShowType)element.GetInt32("ContentShowType");
        this.ContentType       = (EDialogueContentType)element.GetInt32("ContentType");
        this.ContentID         = element.GetInt32("ContentID");
    }
}

public class ReadCfgDialogue : DReadBase<int, DDialogue>
{

}