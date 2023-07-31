using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

namespace Yarvis.LevelSystem
{
	public class LevelEngine
	{
		public bool StoreUserDetails(DUser user)
		{
			try
			{
                var path = @"/Users/atakanaktakka/Projects/Yarvis/LevelSystem/UserInfo.json";


                var json = File.ReadAllText(path);
                var jsonObj = JObject.Parse(json);

                var members = jsonObj["members"].ToObject<List<DUser>>();
				members.Add(user);

				jsonObj["members"] = JArray.FromObject(members);
				File.WriteAllText(path, jsonObj.ToString());

				return true;
            }
			catch(Exception ex)
			{
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
			
        }
        public bool CheckUserExists(string username, ulong guildID)
        {
            using (StreamReader sr = new StreamReader("UserInfo.json"))
            {
                string json = sr.ReadToEnd();
                LevelJSONFile userToGet = JsonConvert.DeserializeObject<LevelJSONFile>(json);

                foreach (var user in userToGet.members)
                {
                    if (user.UserName == username && user.guildID == guildID)
                    {
                        return true;
                    }
                    else { }
                }
            }
            return false;
        }
        public DUser GetUser(string username, ulong guildID)
		{
            using (StreamReader sr = new StreamReader("UserInfo.json"))
            {
                string json = sr.ReadToEnd();
                LevelJSONFile userToGet = JsonConvert.DeserializeObject<LevelJSONFile>(json);

                foreach (var user in userToGet.members)
                {
                    if (user.UserName == username && user.guildID == guildID)
                    {
						return new DUser()
						{
							UserName = user.UserName,
							guildID = user.guildID,
							XP = user.XP,
							Level = user.Level
						};
                    }
                    else
                    {

                    }
                }
            }
            return null;
        }
	}
	class LevelJSONFile
	{
		public string userInfo { get; set; }
		public DUser[] members { get; set; }
	}
}