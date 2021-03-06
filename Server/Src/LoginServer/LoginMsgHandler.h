﻿#include "LogicSvrManager.h"
#ifndef _LOGIN_MSG_HANDLER_H_
#define _LOGIN_MSG_HANDLER_H_

class CLoginMsgHandler
{
public:
	CLoginMsgHandler();

	~CLoginMsgHandler();

	BOOL Init();

	BOOL Uninit();

	BOOL DispatchPacket(NetPacket *pNetPacket);

	//*********************消息处理定义开始******************************
public:
	BOOL OnMsgCheckVersionReq(NetPacket *pPacket);
	BOOL OnMsgAccountRegReq(NetPacket *pPacket);
	BOOL OnMsgAccountLoginReq(NetPacket *pPacket);
	BOOL OnMsgAccountLoginRegReq(NetPacket *pPacket);
	BOOL OnMsgServerListReq(NetPacket *pPacket);
	BOOL OnMsgSelectServerReq(NetPacket *pPacket);

	BOOL OnMsgAccountRegAck(NetPacket *pPacket);
	BOOL OnMsgAccountLoginAck(NetPacket *pPacket);

	BOOL OnMsgLogicSvrRegReq(NetPacket *pPacket);
	BOOL OnMsgSelectServerAck(NetPacket *pPacket);

	
	//*********************消息处理定义结束******************************

public:
	LogicSvrManager m_LogicSvrMgr;
};

#endif //_LOGIN_MSG_HANDLER_H_
