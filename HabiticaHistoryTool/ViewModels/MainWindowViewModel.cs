using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Linq;

namespace HabiticaHistoryTool.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _Msg;
        public string Msg
        {
            get { return _Msg; }
            set { SetProperty(ref _Msg, value); }
        }

        private string _APIURL= "https://habitica.com/export/userdata.json";
        public string APIURL
        {
            get { return _APIURL; }
            set { SetProperty(ref _APIURL, value); }
        }

        private string _UserName= "eef0d857-2c84-4c54-9b5a-add53b3df684";
        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }

        private string _UserToken= "b7e7cc63-dc9d-456c-a1cb-d17817484535";
        public string UserToken
        {
            get { return _UserToken; }
            set { SetProperty(ref _UserToken, value); }
        }

        private List<HabiData> _TodosSource;
        public List<HabiData> TodosSource
        {
            get { return _TodosSource; }
            set { SetProperty(ref _TodosSource, value); }
        }

        private List<HabiData> _HabitsSource;
        public List<HabiData> HabitsSource
        {
            get { return _HabitsSource; }
            set { SetProperty(ref _HabitsSource, value); }
        }

        private List<HabiData> _DailySource;
        public List<HabiData> DailySource
        {
            get { return _DailySource; }
            set { SetProperty(ref _DailySource, value); }
        }

        private DelegateCommand _GetCommand;
        public DelegateCommand GetCommand =>
            _GetCommand ?? (_GetCommand = new DelegateCommand(ExecuteGetCommand));

        async void ExecuteGetCommand()
        {
            if (!string.IsNullOrEmpty(APIURL))
            {
                try
                {
                    // 尝试获取JSON数据
                    string jsonData = await Base.FetchHabiticaData(APIURL,UserName,UserToken);
                    JObject user_data = JObject.Parse(jsonData);

                    // 解析并输出已完成的todos
                    Base.GetCompletedTodos(user_data, out List<HabiData> todos, out List<HabiData> habits, out List<HabiData> dailys);
                    var todosSource = todos;
                    var habitsSource = habits;
                    var dailySource = dailys;

                    var todoAdd= todos.Except(TodosSource, new IdComparer()).Count();
                    var habitAdd = habitsSource.Except(HabitsSource, new IdComparer()).Count();
                    var dailyAdd = dailys.Except(DailySource,new IdComparer()).Count();
                   
                    Msg= @"已更新待办事项数:" + todoAdd + "  已更新习惯数:" + habitAdd + "  已更新日报数:" + dailyAdd;

                    TodosSource = todos;
                    HabitsSource = habits;
                    DailySource = dailys;

                    Base.Write(new HabiDataAll() { dailys = DailySource, todos = TodosSource, habits = HabitsSource });
                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine("\nError fetching data from Habitica: " + httpEx.Message);
                }
                catch (JsonException jsonEx)
                {
                    Console.WriteLine("\nError parsing JSON data: " + jsonEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nAn unexpected error occurred: " + ex.Message);
                }
            }
        }

        public MainWindowViewModel()
        {
            var all= Base.Read();
            TodosSource = all.todos;
            HabitsSource=all.habits;
            DailySource=all.dailys;
        }
    }
}
