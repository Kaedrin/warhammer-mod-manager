using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Warhammer2MM
{
	[DataContract]
	public class SteamData
	{
		[DataMember]
		public ulong SteamID;

		// Not serialized: true when Steam has a newer workshop version than the local copy.
		public bool UpdateAvailable;

		[DataMember]
		public DateTime HakLastUpdated;

		[DataMember]
		public DateTime PNGLaastUpdated;

		[DataMember]
		public List<string> UserNotes;

		[DataMember]
		public string HakFileName;

		[DataMember]
		public string PNGFileName;

		[DataMember]
		public string SteamTitle;

		[DataMember]
		public string SteamAuthor;

		[DataMember]
		public string DisplayFileName;

		[DataMember]
		public DateTime SteamLastUploadDate;

		[DataMember]
		public string SteamDescription;

		[DataMember]
		public bool Outdated;

		[DataMember]
		public string SteamTags;

		[DataMember]
		public string SteamURL;

		[DataMember]
		public string ContentHakFile;

		[DataMember]
		public string ContentPNGFile;

		[DataMember]
		public uint UnixHakLastUpdated;

		[DataMember]
		public uint UnixPNGLaastUpdated;

		[DataMember]
		public bool BinFormat;

		public SteamData()
		{
			UserNotes = new List<string>();
			Outdated = false;
		}

		public string SimpleHakFileName()
		{
			return HakFileName;
		}
	}
}
