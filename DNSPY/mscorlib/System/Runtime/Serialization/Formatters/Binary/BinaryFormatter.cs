using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200077B RID: 1915
	[ComVisible(true)]
	public sealed class BinaryFormatter : IRemotingFormatter, IFormatter
	{
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x0600533D RID: 21309 RVA: 0x00123D70 File Offset: 0x00121F70
		// (set) Token: 0x0600533E RID: 21310 RVA: 0x00123D78 File Offset: 0x00121F78
		public FormatterTypeStyle TypeFormat
		{
			get
			{
				return this.m_typeFormat;
			}
			set
			{
				this.m_typeFormat = value;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x0600533F RID: 21311 RVA: 0x00123D81 File Offset: 0x00121F81
		// (set) Token: 0x06005340 RID: 21312 RVA: 0x00123D89 File Offset: 0x00121F89
		public FormatterAssemblyStyle AssemblyFormat
		{
			get
			{
				return this.m_assemblyFormat;
			}
			set
			{
				this.m_assemblyFormat = value;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06005341 RID: 21313 RVA: 0x00123D92 File Offset: 0x00121F92
		// (set) Token: 0x06005342 RID: 21314 RVA: 0x00123D9A File Offset: 0x00121F9A
		public TypeFilterLevel FilterLevel
		{
			get
			{
				return this.m_securityLevel;
			}
			set
			{
				this.m_securityLevel = value;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06005343 RID: 21315 RVA: 0x00123DA3 File Offset: 0x00121FA3
		// (set) Token: 0x06005344 RID: 21316 RVA: 0x00123DAB File Offset: 0x00121FAB
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this.m_surrogates;
			}
			set
			{
				this.m_surrogates = value;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06005345 RID: 21317 RVA: 0x00123DB4 File Offset: 0x00121FB4
		// (set) Token: 0x06005346 RID: 21318 RVA: 0x00123DBC File Offset: 0x00121FBC
		public SerializationBinder Binder
		{
			get
			{
				return this.m_binder;
			}
			set
			{
				this.m_binder = value;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06005347 RID: 21319 RVA: 0x00123DC5 File Offset: 0x00121FC5
		// (set) Token: 0x06005348 RID: 21320 RVA: 0x00123DCD File Offset: 0x00121FCD
		public StreamingContext Context
		{
			get
			{
				return this.m_context;
			}
			set
			{
				this.m_context = value;
			}
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x00123DD6 File Offset: 0x00121FD6
		public BinaryFormatter()
		{
			this.m_surrogates = null;
			this.m_context = new StreamingContext(StreamingContextStates.All);
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x00123E03 File Offset: 0x00122003
		public BinaryFormatter(ISurrogateSelector selector, StreamingContext context)
		{
			this.m_surrogates = selector;
			this.m_context = context;
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x00123E27 File Offset: 0x00122027
		public object Deserialize(Stream serializationStream)
		{
			return this.Deserialize(serializationStream, null);
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x00123E31 File Offset: 0x00122031
		[SecurityCritical]
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck)
		{
			return this.Deserialize(serializationStream, handler, fCheck, null);
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x00123E3D File Offset: 0x0012203D
		[SecuritySafeCritical]
		public object Deserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.Deserialize(serializationStream, handler, true);
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x00123E48 File Offset: 0x00122048
		[SecuritySafeCritical]
		public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, true, methodCallMessage);
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x00123E54 File Offset: 0x00122054
		[SecurityCritical]
		[ComVisible(false)]
		public object UnsafeDeserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.Deserialize(serializationStream, handler, false);
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x00123E5F File Offset: 0x0012205F
		[SecurityCritical]
		[ComVisible(false)]
		public object UnsafeDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, false, methodCallMessage);
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x00123E6B File Offset: 0x0012206B
		[SecurityCritical]
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, IMethodCallMessage methodCallMessage)
		{
			return this.Deserialize(serializationStream, handler, fCheck, false, methodCallMessage);
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x00123E7C File Offset: 0x0012207C
		[SecurityCritical]
		internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", Environment.GetResourceString("ArgumentNull_WithParamName", new object[] { serializationStream }));
			}
			if (serializationStream.CanSeek && serializationStream.Length == 0L)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_Stream"));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			internalFE.FEsecurityLevel = this.m_securityLevel;
			ObjectReader objectReader = new ObjectReader(serializationStream, this.m_surrogates, this.m_context, internalFE, this.m_binder);
			objectReader.crossAppDomainArray = this.m_crossAppDomainArray;
			return objectReader.Deserialize(handler, new __BinaryParser(serializationStream, objectReader), fCheck, isCrossAppDomain, methodCallMessage);
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x00123F35 File Offset: 0x00122135
		public void Serialize(Stream serializationStream, object graph)
		{
			this.Serialize(serializationStream, graph, null);
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x00123F40 File Offset: 0x00122140
		[SecuritySafeCritical]
		public void Serialize(Stream serializationStream, object graph, Header[] headers)
		{
			this.Serialize(serializationStream, graph, headers, true);
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x00123F4C File Offset: 0x0012214C
		[SecurityCritical]
		internal void Serialize(Stream serializationStream, object graph, Header[] headers, bool fCheck)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream", Environment.GetResourceString("ArgumentNull_WithParamName", new object[] { serializationStream }));
			}
			InternalFE internalFE = new InternalFE();
			internalFE.FEtypeFormat = this.m_typeFormat;
			internalFE.FEserializerTypeEnum = InternalSerializerTypeE.Binary;
			internalFE.FEassemblyFormat = this.m_assemblyFormat;
			ObjectWriter objectWriter = new ObjectWriter(this.m_surrogates, this.m_context, internalFE, this.m_binder);
			__BinaryWriter _BinaryWriter = new __BinaryWriter(serializationStream, objectWriter, this.m_typeFormat);
			objectWriter.Serialize(graph, headers, _BinaryWriter, fCheck);
			this.m_crossAppDomainArray = objectWriter.crossAppDomainArray;
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x00123FE0 File Offset: 0x001221E0
		internal static TypeInformation GetTypeInformation(Type type)
		{
			if (AppContextSwitches.UseConcurrentFormatterTypeCache)
			{
				return BinaryFormatter.concurrentTypeNameCache.Value.GetOrAdd(type, delegate(Type t)
				{
					bool flag3;
					string clrAssemblyName2 = FormatterServices.GetClrAssemblyName(t, out flag3);
					return new TypeInformation(FormatterServices.GetClrTypeFullName(t), clrAssemblyName2, flag3);
				});
			}
			Dictionary<Type, TypeInformation> dictionary = BinaryFormatter.typeNameCache;
			TypeInformation typeInformation2;
			lock (dictionary)
			{
				TypeInformation typeInformation = null;
				if (!BinaryFormatter.typeNameCache.TryGetValue(type, out typeInformation))
				{
					bool flag2;
					string clrAssemblyName = FormatterServices.GetClrAssemblyName(type, out flag2);
					typeInformation = new TypeInformation(FormatterServices.GetClrTypeFullName(type), clrAssemblyName, flag2);
					BinaryFormatter.typeNameCache.Add(type, typeInformation);
				}
				typeInformation2 = typeInformation;
			}
			return typeInformation2;
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x0012408C File Offset: 0x0012228C
		// Note: this type is marked as 'beforefieldinit'.
		static BinaryFormatter()
		{
		}

		// Token: 0x04002582 RID: 9602
		internal ISurrogateSelector m_surrogates;

		// Token: 0x04002583 RID: 9603
		internal StreamingContext m_context;

		// Token: 0x04002584 RID: 9604
		internal SerializationBinder m_binder;

		// Token: 0x04002585 RID: 9605
		internal FormatterTypeStyle m_typeFormat = FormatterTypeStyle.TypesAlways;

		// Token: 0x04002586 RID: 9606
		internal FormatterAssemblyStyle m_assemblyFormat;

		// Token: 0x04002587 RID: 9607
		internal TypeFilterLevel m_securityLevel = TypeFilterLevel.Full;

		// Token: 0x04002588 RID: 9608
		internal object[] m_crossAppDomainArray;

		// Token: 0x04002589 RID: 9609
		private static Dictionary<Type, TypeInformation> typeNameCache = new Dictionary<Type, TypeInformation>();

		// Token: 0x0400258A RID: 9610
		private static Lazy<ConcurrentDictionary<Type, TypeInformation>> concurrentTypeNameCache = new Lazy<ConcurrentDictionary<Type, TypeInformation>>(() => new ConcurrentDictionary<Type, TypeInformation>());

		// Token: 0x02000C69 RID: 3177
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06007089 RID: 28809 RVA: 0x00183472 File Offset: 0x00181672
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600708A RID: 28810 RVA: 0x0018347E File Offset: 0x0018167E
			public <>c()
			{
			}

			// Token: 0x0600708B RID: 28811 RVA: 0x00183488 File Offset: 0x00181688
			internal TypeInformation <GetTypeInformation>b__40_0(Type t)
			{
				bool flag;
				string clrAssemblyName = FormatterServices.GetClrAssemblyName(t, out flag);
				return new TypeInformation(FormatterServices.GetClrTypeFullName(t), clrAssemblyName, flag);
			}

			// Token: 0x0600708C RID: 28812 RVA: 0x001834AB File Offset: 0x001816AB
			internal ConcurrentDictionary<Type, TypeInformation> <.cctor>b__41_0()
			{
				return new ConcurrentDictionary<Type, TypeInformation>();
			}

			// Token: 0x040037D7 RID: 14295
			public static readonly BinaryFormatter.<>c <>9 = new BinaryFormatter.<>c();

			// Token: 0x040037D8 RID: 14296
			public static Func<Type, TypeInformation> <>9__40_0;
		}
	}
}
