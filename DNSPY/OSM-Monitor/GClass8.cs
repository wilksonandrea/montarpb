using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Filters;
using Plugin.Core.JSON;
using Plugin.Core.Managers;
using Plugin.Core.XML;

// Token: 0x0200001E RID: 30
public class GClass8
{
	// Token: 0x06000093 RID: 147 RVA: 0x00004F74 File Offset: 0x00003174
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
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00004FC8 File Offset: 0x000031C8
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
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00005010 File Offset: 0x00003210
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
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00005068 File Offset: 0x00003268
	public static bool smethod_3()
	{
		bool flag;
		try
		{
			GameRuleXML.Reload();
			flag = true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x000050A4 File Offset: 0x000032A4
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
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00002133 File Offset: 0x00000333
	public GClass8()
	{
	}
}
