using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyLib.Extensions.Serialization
{
	public static class BinaryFormaterExtensions
	{
		public static T DeepCopy<T>(
			this T source
		) //where T : ISerializable
			=> 
			new BinaryFormatter()
			.DeepCopy(source);

		public static T DeepCopy<T>(
			this BinaryFormatter bf
			, T original
		) // where T : ISerializable
		{
			using (var stream = new MemoryStream())
			{
				bf.Serialize(stream, original);
				stream.Flush();
				stream.Position = 0;
				return (T)bf.Deserialize(stream);
			}
		}
	}
}
