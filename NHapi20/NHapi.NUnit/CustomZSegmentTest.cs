using System;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V22_ZSegments;
using NHapi.Model.V22_ZSegments.Message;
using NUnit.Framework;

namespace NHapi.NUnit
{
	[TestFixture]
	public class CustomZSegmentTest
	{
		[Test]
		public void ParseADT_A08()
		{
			//this is some fictive data
			string message = @"MSH|^~\&|SUNS1|OVI02|AZIS|CMD|200606221348||ADT^A08|1049691900|P|2.2
EVN|A08|200601060800
PID||8912716038^^^51276|0216128^^^51276||BARDOUN^LEA SACHA||19981201|F|||AVENUE FRANC GOLD 8^^LUXEMBOURGH^^6780^150||053/12456789||N|S|||99120162652||^^^|||||B
PV1||O|^^|U|||07632^MORTELO^POL^^^DR.|^^^^^|||||N||||||0200001198
PV2|||^^AZIS||N|||200601060800
IN1|0001|2|314000|||||||||19800101|||1|BARDOUN^LEA SACHA|1|19981201|AVENUE FRANC GOLD 8^^LUXEMBOURGH^^6780^150|||||||||||||||||
ZIN|0164652011399|0164652011399|101|101|45789^Broken bone";


			/*	**********************************************************
			 * Requirements:
			 * 
			 * 1) You need to update the project's app.config <configSections>
			 * element to include the 'HL7PackageConfiguration' configuration section
			 * 
			 * <configSections>
			 * 	<section name="HL7PackageConfiguration" type="NHapi.Base.Model.Configuration.HL7PackageConfigurationSection, NHapi.Base"/>
			 * </configSections>
			 * 
			 * And then you need to add a configuration that details the namespace and
			 * custom version name so that the PipeParser can find the custom definitions:
			 * 
			 * <HL7PackageConfiguration>
			 * 	<HL7PackageCollection>
			 * 		<HL7Package name="NHapi.Model.V22_ZSegments" version="2.2.CustomZ"/>
			 * 	</HL7PackageCollection>
			 * </HL7PackageConfiguration>
			 * 
			 * 2) You need to use this PipeParse.Parse(message, version) method
			 * and use the version you defined in the app.config above.
			 * 
          * ***********************************************************/
			var parser = new PipeParser();

			IMessage m = parser.Parse(message, Constants.VERSION);

			Assert.IsNotNull(m);

			Console.WriteLine("Type: " + m.GetType());

			var adtA08 = m as ADT_A08;
			//verify some Z segment data
			Assert.AreEqual("45789", adtA08.ZIN.AccidentData.Id.Value);
			Assert.AreEqual("Broken bone", adtA08.ZIN.AccidentData.Text.Value);
		}
	}
}