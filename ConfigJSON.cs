using System;
using Newtonsoft.Json;

namespace Yarvis
{
	internal class ConfigJSON
	{
		[JsonProperty("token")]
		public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
    }
}

