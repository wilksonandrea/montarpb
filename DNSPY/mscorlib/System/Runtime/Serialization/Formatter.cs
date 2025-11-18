using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200074A RID: 1866
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class Formatter : IFormatter
	{
		// Token: 0x06005243 RID: 21059 RVA: 0x00120AAD File Offset: 0x0011ECAD
		protected Formatter()
		{
			this.m_objectQueue = new Queue();
			this.m_idGenerator = new ObjectIDGenerator();
		}

		// Token: 0x06005244 RID: 21060
		public abstract object Deserialize(Stream serializationStream);

		// Token: 0x06005245 RID: 21061 RVA: 0x00120ACC File Offset: 0x0011ECCC
		protected virtual object GetNext(out long objID)
		{
			if (this.m_objectQueue.Count == 0)
			{
				objID = 0L;
				return null;
			}
			object obj = this.m_objectQueue.Dequeue();
			bool flag;
			objID = this.m_idGenerator.HasId(obj, out flag);
			if (flag)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NoID"));
			}
			return obj;
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x00120B1C File Offset: 0x0011ED1C
		protected virtual long Schedule(object obj)
		{
			if (obj == null)
			{
				return 0L;
			}
			bool flag;
			long id = this.m_idGenerator.GetId(obj, out flag);
			if (flag)
			{
				this.m_objectQueue.Enqueue(obj);
			}
			return id;
		}

		// Token: 0x06005247 RID: 21063
		public abstract void Serialize(Stream serializationStream, object graph);

		// Token: 0x06005248 RID: 21064
		protected abstract void WriteArray(object obj, string name, Type memberType);

		// Token: 0x06005249 RID: 21065
		protected abstract void WriteBoolean(bool val, string name);

		// Token: 0x0600524A RID: 21066
		protected abstract void WriteByte(byte val, string name);

		// Token: 0x0600524B RID: 21067
		protected abstract void WriteChar(char val, string name);

		// Token: 0x0600524C RID: 21068
		protected abstract void WriteDateTime(DateTime val, string name);

		// Token: 0x0600524D RID: 21069
		protected abstract void WriteDecimal(decimal val, string name);

		// Token: 0x0600524E RID: 21070
		protected abstract void WriteDouble(double val, string name);

		// Token: 0x0600524F RID: 21071
		protected abstract void WriteInt16(short val, string name);

		// Token: 0x06005250 RID: 21072
		protected abstract void WriteInt32(int val, string name);

		// Token: 0x06005251 RID: 21073
		protected abstract void WriteInt64(long val, string name);

		// Token: 0x06005252 RID: 21074
		protected abstract void WriteObjectRef(object obj, string name, Type memberType);

		// Token: 0x06005253 RID: 21075 RVA: 0x00120B50 File Offset: 0x0011ED50
		protected virtual void WriteMember(string memberName, object data)
		{
			if (data == null)
			{
				this.WriteObjectRef(data, memberName, typeof(object));
				return;
			}
			Type type = data.GetType();
			if (type == typeof(bool))
			{
				this.WriteBoolean(Convert.ToBoolean(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(char))
			{
				this.WriteChar(Convert.ToChar(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(sbyte))
			{
				this.WriteSByte(Convert.ToSByte(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(byte))
			{
				this.WriteByte(Convert.ToByte(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(short))
			{
				this.WriteInt16(Convert.ToInt16(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(int))
			{
				this.WriteInt32(Convert.ToInt32(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(long))
			{
				this.WriteInt64(Convert.ToInt64(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(float))
			{
				this.WriteSingle(Convert.ToSingle(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(double))
			{
				this.WriteDouble(Convert.ToDouble(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(DateTime))
			{
				this.WriteDateTime(Convert.ToDateTime(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(decimal))
			{
				this.WriteDecimal(Convert.ToDecimal(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(ushort))
			{
				this.WriteUInt16(Convert.ToUInt16(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(uint))
			{
				this.WriteUInt32(Convert.ToUInt32(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type == typeof(ulong))
			{
				this.WriteUInt64(Convert.ToUInt64(data, CultureInfo.InvariantCulture), memberName);
				return;
			}
			if (type.IsArray)
			{
				this.WriteArray(data, memberName, type);
				return;
			}
			if (type.IsValueType)
			{
				this.WriteValueType(data, memberName, type);
				return;
			}
			this.WriteObjectRef(data, memberName, type);
		}

		// Token: 0x06005254 RID: 21076
		[CLSCompliant(false)]
		protected abstract void WriteSByte(sbyte val, string name);

		// Token: 0x06005255 RID: 21077
		protected abstract void WriteSingle(float val, string name);

		// Token: 0x06005256 RID: 21078
		protected abstract void WriteTimeSpan(TimeSpan val, string name);

		// Token: 0x06005257 RID: 21079
		[CLSCompliant(false)]
		protected abstract void WriteUInt16(ushort val, string name);

		// Token: 0x06005258 RID: 21080
		[CLSCompliant(false)]
		protected abstract void WriteUInt32(uint val, string name);

		// Token: 0x06005259 RID: 21081
		[CLSCompliant(false)]
		protected abstract void WriteUInt64(ulong val, string name);

		// Token: 0x0600525A RID: 21082
		protected abstract void WriteValueType(object obj, string name, Type memberType);

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600525B RID: 21083
		// (set) Token: 0x0600525C RID: 21084
		public abstract ISurrogateSelector SurrogateSelector { get; set; }

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x0600525D RID: 21085
		// (set) Token: 0x0600525E RID: 21086
		public abstract SerializationBinder Binder { get; set; }

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x0600525F RID: 21087
		// (set) Token: 0x06005260 RID: 21088
		public abstract StreamingContext Context { get; set; }

		// Token: 0x04002475 RID: 9333
		protected ObjectIDGenerator m_idGenerator;

		// Token: 0x04002476 RID: 9334
		protected Queue m_objectQueue;
	}
}
