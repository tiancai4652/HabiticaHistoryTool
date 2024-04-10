using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace HabiticaHistoryTool.ViewModels
{
    public class Base
    {
        public static async Task<string> FetchHabiticaData(string url, string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    client.DefaultRequestHeaders.Add("x-api-user", username);
                    client.DefaultRequestHeaders.Add("x-api-key", password);
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string jsonData = await response.Content.ReadAsStringAsync();
                    return jsonData;
                }
                catch (HttpRequestException)
                {
                    throw; // 重新抛出异常以便在Main方法中处理
                }
            }
        }

        public static void GetCompletedTodos(JObject userData,out List<HabiData> todos, out List<HabiData> habits, out List<HabiData> dailys)
        {
            todos = new List<HabiData>();
            //todos,habits,rewards,dailys
            JArray todosArray = (JArray)userData["tasks"]["todos"];
            foreach (JObject todo in todosArray)
            {
                // 检查todo是否已完成
                if (todo["completed"] != null && todo["completed"].Value<bool>())
                {
                    // 输出已完成的待办事项的详细信息
                    //completedTodos.Add($"ID: {todo["id"].ToString()}, Text: {todo["text"]}");
                    todos.Add(new HabiData() { ID = todo["id"].ToString(), Name = todo["text"].ToString(), CreatedDate= DateTime.Parse( todo["createdAt"].ToString()),UpdateDate= DateTime.Parse(todo["updatedAt"].ToString()) });
                }
            }
            todos = todos.OrderByDescending(x => x.UpdateDate).ToList();

            habits = new List<HabiData>();
            //todos,habits,rewards,dailys
            todosArray = (JArray)userData["tasks"]["habits"];
            foreach (JObject todo in todosArray)
            {
                habits.Add(new HabiData() { ID = todo["id"].ToString(), Name = todo["text"].ToString(),CreatedDate = DateTime.Parse(todo["createdAt"].ToString()), UpdateDate = DateTime.Parse(todo["updatedAt"].ToString()) });
            }
            habits= habits.OrderByDescending(x => x.UpdateDate).ToList();


            dailys = new List<HabiData>();
            //todos,habits,rewards,dailys
            todosArray = (JArray)userData["tasks"]["dailys"];
            foreach (JObject todo in todosArray)
            {
                dailys.Add(new HabiData() { ID = todo["id"].ToString(), Name = todo["text"].ToString() });
            }

        }

        public static HabiDataAll Read()
        {
            var file = SysFile.Instance.FilePath;
            if (!File.Exists(file))
            {
                return new HabiDataAll();
            }
            var str= File.ReadAllText(file);
            HabiDataAll m = JsonConvert.DeserializeObject<HabiDataAll>(str);
            return m;
        }

        public static void Write(HabiDataAll data)
        {
            var file = SysFile.Instance.FilePath;
            var str = JsonConvert.SerializeObject(data);
            File.WriteAllText(file, str);
        }

    }

    public class HabiData
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class IdComparer : IEqualityComparer<HabiData>
    {
        public bool Equals(HabiData x, HabiData y)
        {
            // 检查两个对象的Id属性是否相等
            return x.ID == y.ID;
        }

        public int GetHashCode(HabiData obj)
        {
            // 为Id属性计算哈希码
            return obj.ID.GetHashCode();
        }
    }


    public class HabiDataAll
    {
        public List<HabiData> todos { get; set; } = new List<HabiData>();

        public List<HabiData> habits { get; set; } = new List<HabiData>();
        public List<HabiData> dailys { get; set; } = new List<HabiData>();
    }
}

