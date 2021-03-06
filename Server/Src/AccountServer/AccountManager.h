﻿#ifndef __DB_ACCOUNT_OBJECT_H__
#define __DB_ACCOUNT_OBJECT_H__
#include "Utility/AVLTree.h"
#include "GameDefine.h"
#include "Utility/Position.h"

struct CAccountObject
{
	UINT64		m_ID;
	std::string m_strName;
	std::string m_strPassword;	
	UINT32      m_dwLastSvrID;
	UINT32      m_dwChannel;		//渠道ID
	BOOL		m_bEnabled;		//是否禁用
	UINT32      m_dwCreateTime; //创建时间
};


class CAccountObjectMgr : public AVLTree<UINT64, CAccountObject>
{
public:
	CAccountObjectMgr();
	~CAccountObjectMgr();

public:
	BOOL			 InitManager();

public:
	CAccountObject*   GetAccountObjectByID(UINT64 m_u64AccountID);

	CAccountObject*   CreateAccountObject(std::string strName, std::string strPwd, UINT32 dwChannel);

	BOOL			  ReleaseAccountObject(UINT64 m_u64AccountID);

	BOOL			  AddAccountObject(UINT64 u64ID, std::string strName, std::string strPwd, UINT32 dwChannel);

	CAccountObject*   GetAccountObjectByName(std::string name);
public:

	std::map<std::string, CAccountObject*>m_mapNameObj;

	UINT64	m_u64MaxID;
};

#endif //__DB_ACCOUNT_OBJECT_H__
