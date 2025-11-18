using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Filters;
using Plugin.Core.JSON;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Models.Map;
using Plugin.Core.XML;
using System;
using System.Collections.Generic;

public class GClass8
{
	public GClass8()
	{
	}

	public static bool smethod_0()
	{
		bool flag;
		try
		{
			ServerConfigJSON.Configs.Clear();
			ServerConfigJSON.Load();
			NickFilter.Filters.Clear();
			NickFilter.Load();
			flag = true;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			flag = false;
		}
		return flag;
	}

	public static bool smethod_1()
	{
		bool flag;
		try
		{
			ShopManager.Reset();
			ShopManager.Load(1);
			ShopManager.Load(2);
			flag = true;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			flag = false;
		}
		return flag;
	}

	public static bool smethod_2()
	{
		bool flag;
		try
		{
			EventLoginXML.Reload();
			EventBoostXML.Reload();
			EventPlaytimeXML.Reload();
			EventQuestXML.Reload();
			EventRankUpXML.Reload();
			EventVisitXML.Reload();
			EventXmasXML.Reload();
			flag = true;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			flag = false;
		}
		return flag;
	}

	public static bool smethod_3()
	{
		bool flag;
		try
		{
			GameRuleXML.Reload();
			flag = true;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			flag = false;
		}
		return flag;
	}

	public static bool smethod_4()
	{
		bool flag;
		try
		{
			TemplatePackXML.Basics.Clear();
			TemplatePackXML.Awards.Clear();
			TemplatePackXML.Cafes.Clear();
			TemplatePackXML.Load();
			SystemMapXML.Rules.Clear();
			SystemMapXML.Matches.Clear();
			SystemMapXML.Load();
			RandomBoxXML.RBoxes.Clear();
			RandomBoxXML.Load();
			InternetCafeXML.Cafes.Clear();
			InternetCafeXML.Load();
			flag = true;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			flag = false;
		}
		return flag;
	}
}