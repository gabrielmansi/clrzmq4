﻿namespace ZeroMQ
{
	using System;

	/// <summary>
	/// Queuing Push-Pull Device
	/// </summary>
	public class PushPullDevice : ZDevice
	{
		/// <summary>
		/// The frontend <see cref="ZSocketType"/> for a streamer device.
		/// </summary>
		public static readonly ZSocketType FrontendType = ZSocketType.PULL;

		/// <summary>
		/// The backend <see cref="ZSocketType"/> for a streamer device.
		/// </summary>
		public static readonly ZSocketType BackendType = ZSocketType.PUSH;

		/// <summary>
		/// Initializes a new instance of the <see cref="StreamerDevice"/> class.
		/// </summary>
		public PushPullDevice(ZContext context, string frontendBindAddr, string backendBindAddr)
			: base(context, FrontendType, BackendType)
		{
			FrontendSetup.Bind(frontendBindAddr);
			BackendSetup.Bind(backendBindAddr);
		}

		/// <summary>
		/// Forwards requests from the frontend socket to the backend socket.
		/// </summary>
		protected override bool FrontendHandler(ZSocket args, out ZMessage message, out ZError error)
		{
			return FrontendSocket.Forward(BackendSocket, out message, out error);
		}

		/// <summary>
		/// Not implemented for the <see cref="StreamerDevice"/>.
		/// </summary>
		protected override bool BackendHandler(ZSocket args, out ZMessage message, out ZError error)
		{
			throw new NotSupportedException();
		}
	}
}