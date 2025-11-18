using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Hosting
{
	// Token: 0x02000A57 RID: 2647
	internal sealed class ManifestRunner
	{
		// Token: 0x060066C4 RID: 26308 RVA: 0x00159B00 File Offset: 0x00157D00
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		internal ManifestRunner(AppDomain domain, ActivationContext activationContext)
		{
			this.m_domain = domain;
			string text;
			string text2;
			CmsUtils.GetEntryPoint(activationContext, out text, out text2);
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoMain"));
			}
			if (string.IsNullOrEmpty(text2))
			{
				this.m_args = new string[0];
			}
			else
			{
				this.m_args = text2.Split(new char[] { ' ' });
			}
			this.m_apt = ApartmentState.Unknown;
			string applicationDirectory = activationContext.ApplicationDirectory;
			this.m_path = Path.Combine(applicationDirectory, text);
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x060066C5 RID: 26309 RVA: 0x00159B84 File Offset: 0x00157D84
		internal RuntimeAssembly EntryAssembly
		{
			[SecurityCritical]
			[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
			[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				if (this.m_assembly == null)
				{
					this.m_assembly = (RuntimeAssembly)Assembly.LoadFrom(this.m_path);
				}
				return this.m_assembly;
			}
		}

		// Token: 0x060066C6 RID: 26310 RVA: 0x00159BB0 File Offset: 0x00157DB0
		[SecurityCritical]
		private void NewThreadRunner()
		{
			this.m_runResult = this.Run(false);
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x00159BC0 File Offset: 0x00157DC0
		[SecurityCritical]
		private int RunInNewThread()
		{
			Thread thread = new Thread(new ThreadStart(this.NewThreadRunner));
			thread.SetApartmentState(this.m_apt);
			thread.Start();
			thread.Join();
			return this.m_runResult;
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x00159C00 File Offset: 0x00157E00
		[SecurityCritical]
		private int Run(bool checkAptModel)
		{
			if (checkAptModel && this.m_apt != ApartmentState.Unknown)
			{
				if (Thread.CurrentThread.GetApartmentState() != ApartmentState.Unknown && Thread.CurrentThread.GetApartmentState() != this.m_apt)
				{
					return this.RunInNewThread();
				}
				Thread.CurrentThread.SetApartmentState(this.m_apt);
			}
			return this.m_domain.nExecuteAssembly(this.EntryAssembly, this.m_args);
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x00159C68 File Offset: 0x00157E68
		[SecurityCritical]
		internal int ExecuteAsAssembly()
		{
			object[] array = this.EntryAssembly.EntryPoint.GetCustomAttributes(typeof(STAThreadAttribute), false);
			if (array.Length != 0)
			{
				this.m_apt = ApartmentState.STA;
			}
			array = this.EntryAssembly.EntryPoint.GetCustomAttributes(typeof(MTAThreadAttribute), false);
			if (array.Length != 0)
			{
				if (this.m_apt == ApartmentState.Unknown)
				{
					this.m_apt = ApartmentState.MTA;
				}
				else
				{
					this.m_apt = ApartmentState.Unknown;
				}
			}
			return this.Run(true);
		}

		// Token: 0x04002E19 RID: 11801
		private AppDomain m_domain;

		// Token: 0x04002E1A RID: 11802
		private string m_path;

		// Token: 0x04002E1B RID: 11803
		private string[] m_args;

		// Token: 0x04002E1C RID: 11804
		private ApartmentState m_apt;

		// Token: 0x04002E1D RID: 11805
		private RuntimeAssembly m_assembly;

		// Token: 0x04002E1E RID: 11806
		private int m_runResult;
	}
}
