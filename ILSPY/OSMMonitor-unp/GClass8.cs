using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Filters;
using Plugin.Core.JSON;
using Plugin.Core.Managers;
using Plugin.Core.XML;

public class GClass8
{
	public static bool smethod_0()
	{
		try
		{
			ServerConfigJSON.Configs.Clear();
			ServerConfigJSON.Load();
			NickFilter.Filters.Clear();
			NickFilter.Load();
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool smethod_1()
	{
		try
		{
			ShopManager.Reset();
			ShopManager.Load(1);
			ShopManager.Load(2);
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool smethod_2()
	{
		try
		{
			EventLoginXML.Reload();
			EventBoostXML.Reload();
			EventPlaytimeXML.Reload();
			EventQuestXML.Reload();
			EventRankUpXML.Reload();
			EventVisitXML.Reload();
			EventXmasXML.Reload();
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool smethod_3()
	{
		try
		{
			GameRuleXML.Reload();
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static bool smethod_4()
	{
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
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}
}
