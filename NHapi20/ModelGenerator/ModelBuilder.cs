﻿using System;
using System.Configuration;
using NHapi.Base;
using NHapi.Base.SourceGeneration;
using ConfigurationSettings = NHapi.Base.ConfigurationSettings;

namespace ModelGenerator
{
	public class ModelBuilder
	{
		public enum MessageType
		{
			All,
			Message,
			Segment,
			DataType,
			EventMapping
		}

		public ModelBuilder()
		{
			BasePath = @"D:\projects\nhapi\SourceForge\nhapi20";
			ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
			MessageTypeToBuild = MessageType.All;
		}

		public string BasePath { get; set; }

		public string Version { get; set; }

		public string ConnectionString { get; set; }

		public MessageType MessageTypeToBuild { get; set; }

		public void Execute()
		{
			if (!string.IsNullOrEmpty(ConnectionString))
			{
				ConfigurationSettings.ConnectionString = ConnectionString;
			}

			Console.WriteLine("Using Database:{0}", NormativeDatabase.Instance.Connection.ConnectionString);
			Console.WriteLine("Base Path:{0}", BasePath);

			switch (MessageTypeToBuild)
			{
				case MessageType.All:
					SourceGenerator.makeAll(BasePath, Version);
					break;
				case MessageType.EventMapping:
					SourceGenerator.MakeEventMapping(BasePath, Version);
					break;
				case MessageType.Segment:
					SegmentGenerator.makeAll(BasePath, Version);
					break;
				case MessageType.Message:
					MessageGenerator.makeAll(BasePath, Version);
					break;
			}
		}
	}
}