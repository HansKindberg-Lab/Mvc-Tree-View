using System;
using System.IO;

namespace Company.Web
{
	public class CaptureStream : Stream
	{
		#region Fields

		private readonly MemoryStream _memoryStream;
		private readonly Stream _stream;

		#endregion

		#region Constructors

		public CaptureStream(Stream stream)
		{
			if(stream == null)
				throw new ArgumentNullException("stream");

			this._memoryStream = new MemoryStream();
			this._stream = stream;
		}

		#endregion

		#region Properties

		public override bool CanRead
		{
			get { return this.Stream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return this.Stream.CanSeek; }
		}

		public override bool CanWrite
		{
			get { return this.Stream.CanWrite; }
		}

		public override long Length
		{
			get { return this.Stream.Length; }
		}

		public virtual MemoryStream MemoryStream
		{
			get { return this._memoryStream; }
		}

		public override long Position
		{
			get { return this.Stream.Position; }
			set { this.Stream.Position = value; }
		}

		protected internal virtual Stream Stream
		{
			get { return this._stream; }
		}

		#endregion

		#region Methods

		public override void Close()
		{
			this.Stream.Close();
		}

		public override void Flush()
		{
			this.Stream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.Stream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.Stream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			this.Stream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this.Stream.Write(buffer, offset, count);
			this.MemoryStream.Write(buffer, offset, count);
		}

		#endregion
	}
}