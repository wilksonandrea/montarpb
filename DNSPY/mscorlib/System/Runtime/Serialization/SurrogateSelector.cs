using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200075B RID: 1883
	[ComVisible(true)]
	public class SurrogateSelector : ISurrogateSelector
	{
		// Token: 0x060052F0 RID: 21232 RVA: 0x0012362C File Offset: 0x0012182C
		public SurrogateSelector()
		{
			this.m_surrogates = new SurrogateHashtable(32);
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x00123644 File Offset: 0x00121844
		public virtual void AddSurrogate(Type type, StreamingContext context, ISerializationSurrogate surrogate)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (surrogate == null)
			{
				throw new ArgumentNullException("surrogate");
			}
			SurrogateKey surrogateKey = new SurrogateKey(type, context);
			this.m_surrogates.Add(surrogateKey, surrogate);
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00123688 File Offset: 0x00121888
		[SecurityCritical]
		private static bool HasCycle(ISurrogateSelector selector)
		{
			ISurrogateSelector surrogateSelector = selector;
			ISurrogateSelector surrogateSelector2 = selector;
			while (surrogateSelector != null)
			{
				surrogateSelector = surrogateSelector.GetNextSelector();
				if (surrogateSelector == null)
				{
					return true;
				}
				if (surrogateSelector == surrogateSelector2)
				{
					return false;
				}
				surrogateSelector = surrogateSelector.GetNextSelector();
				surrogateSelector2 = surrogateSelector2.GetNextSelector();
				if (surrogateSelector == surrogateSelector2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x001236C8 File Offset: 0x001218C8
		[SecurityCritical]
		public virtual void ChainSelector(ISurrogateSelector selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			if (selector == this)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_DuplicateSelector"));
			}
			if (!SurrogateSelector.HasCycle(selector))
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycleInArgument"), "selector");
			}
			ISurrogateSelector surrogateSelector = selector.GetNextSelector();
			ISurrogateSelector surrogateSelector2 = selector;
			while (surrogateSelector != null && surrogateSelector != this)
			{
				surrogateSelector2 = surrogateSelector;
				surrogateSelector = surrogateSelector.GetNextSelector();
			}
			if (surrogateSelector == this)
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), "selector");
			}
			surrogateSelector = selector;
			ISurrogateSelector surrogateSelector3 = selector;
			while (surrogateSelector != null)
			{
				if (surrogateSelector == surrogateSelector2)
				{
					surrogateSelector = this.GetNextSelector();
				}
				else
				{
					surrogateSelector = surrogateSelector.GetNextSelector();
				}
				if (surrogateSelector == null)
				{
					break;
				}
				if (surrogateSelector == surrogateSelector3)
				{
					throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), "selector");
				}
				if (surrogateSelector == surrogateSelector2)
				{
					surrogateSelector = this.GetNextSelector();
				}
				else
				{
					surrogateSelector = surrogateSelector.GetNextSelector();
				}
				if (surrogateSelector3 == surrogateSelector2)
				{
					surrogateSelector3 = this.GetNextSelector();
				}
				else
				{
					surrogateSelector3 = surrogateSelector3.GetNextSelector();
				}
				if (surrogateSelector == surrogateSelector3)
				{
					throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), "selector");
				}
			}
			ISurrogateSelector nextSelector = this.m_nextSelector;
			this.m_nextSelector = selector;
			if (nextSelector != null)
			{
				surrogateSelector2.ChainSelector(nextSelector);
			}
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x001237DA File Offset: 0x001219DA
		[SecurityCritical]
		public virtual ISurrogateSelector GetNextSelector()
		{
			return this.m_nextSelector;
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x001237E4 File Offset: 0x001219E4
		[SecurityCritical]
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			selector = this;
			SurrogateKey surrogateKey = new SurrogateKey(type, context);
			ISerializationSurrogate serializationSurrogate = (ISerializationSurrogate)this.m_surrogates[surrogateKey];
			if (serializationSurrogate != null)
			{
				return serializationSurrogate;
			}
			if (this.m_nextSelector != null)
			{
				return this.m_nextSelector.GetSurrogate(type, context, out selector);
			}
			return null;
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00123840 File Offset: 0x00121A40
		public virtual void RemoveSurrogate(Type type, StreamingContext context)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			SurrogateKey surrogateKey = new SurrogateKey(type, context);
			this.m_surrogates.Remove(surrogateKey);
		}

		// Token: 0x040024CD RID: 9421
		internal SurrogateHashtable m_surrogates;

		// Token: 0x040024CE RID: 9422
		internal ISurrogateSelector m_nextSelector;
	}
}
